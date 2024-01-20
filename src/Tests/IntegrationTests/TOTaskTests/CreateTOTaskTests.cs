using Api;
using Application.TOProjectLogic.Commands.CreateTOProject;
using Application.TOTaskLogic.Commands.CreateTOTask;
using Contracts.TOProject.CreateTOProject;
using Contracts.TOTask.CreateTOTask;
using IntegrationTests.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace IntegrationTests.TOTaskTests
{
    public class CreateTOTaskTests(WebApplicationFactory<Program> factory) : IntegrationTestBase(factory)
    {
        [Fact]
        public async Task TOTask_Create()
        {
            // Arrange
            var jwtToken = await CreateAndLoginTestUser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            CreateTOProjectRequest requestProject = new(
                Name: "test",
                Description: "test");

            var responseProject = await Client.PostAsJsonAsync(Endpoint.TOProjectController.CreateProject, requestProject);
            var resultProject = await responseProject.Content.ReadFromJsonAsync<CreateTOProjectCommandResponse>();
            Assert.NotNull(resultProject?.Id);


            CreateTOTaskRequest request = new(
                Name: "test",
                Description: "test");

            var response = await Client.PostAsJsonAsync(Endpoint.TOTaskController.CreateTask(resultProject.Id), request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            ExpectedErrorsList? expectedErrors = [];
            Assert.Equivalent(expectedErrors, await ActualErrors(response));

            var result = await response.Content.ReadFromJsonAsync<CreateTOTaskCommandResponse>();
            Assert.NotNull(result?.Id);
            Assert.Equal(request.Name, result?.Name);
        }

        [Fact]
        public async Task TOTask_CreateDuplicate()
        {
            // Arrange
            var jwtToken = await CreateAndLoginTestUser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            CreateTOProjectRequest requestProject = new(
                Name: "test",
                Description: "test");

            var responseProject = await Client.PostAsJsonAsync(Endpoint.TOProjectController.CreateProject, requestProject);
            var resultProject = await responseProject.Content.ReadFromJsonAsync<CreateTOProjectCommandResponse>();
            Assert.NotNull(resultProject?.Id);


            CreateTOTaskRequest request1 = new(
                Name: "test",
                Description: "test");

            _ = await Client.PostAsJsonAsync(Endpoint.TOTaskController.CreateTask(resultProject.Id), request1);

            CreateTOTaskRequest request = new(
                Name: "test",
                Description: "test");

            var response = await Client.PostAsJsonAsync(Endpoint.TOTaskController.CreateTask(resultProject.Id), request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            ExpectedErrorsList? expectedErrors = [];
            Assert.Equivalent(expectedErrors, await ActualErrors(response));

            var result = await response.Content.ReadFromJsonAsync<CreateTOTaskCommandResponse>();
            Assert.NotNull(result?.Id);
            Assert.Equal(request.Name, result?.Name);
        }


        [Fact]
        public async Task TOTask_Create_NonExistentProject()
        {
            // Arrange
            var jwtToken = await CreateAndLoginTestUser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            
            CreateTOTaskRequest request = new(
                Name: "test",
                Description: "test");

            var response = await Client.PostAsJsonAsync(Endpoint.TOTaskController.CreateTask(Guid.NewGuid()), request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            var expectedTitle = "Entity with given Id is not in the database.";
            Assert.Equal(expectedTitle, await ActualTitle(response));
        }

        [Fact]
        public async Task TOTask_Create_UserNotMemberNorCreatorOfProject()
        {
            // Arrange
            var jwtToken = await CreateAndLoginTestUser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            // Create project
            CreateTOProjectRequest requestProject = new(
                Name: "test",
                Description: "test");

            var responseProject = await Client.PostAsJsonAsync(Endpoint.TOProjectController.CreateProject, requestProject);
            var resultProject = await responseProject.Content.ReadFromJsonAsync<CreateTOProjectCommandResponse>();
            Assert.NotNull(resultProject?.Id);

            // Create user
            var newUserToken = await CreateAndLoginTestUser(Name:"test2", Email:"test2@test.test");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newUserToken);

            // Create task
            CreateTOTaskRequest request = new(
                Name: "test",
                Description: "test");

            var response = await Client.PostAsJsonAsync(Endpoint.TOTaskController.CreateTask(resultProject.Id), request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            var expectedTitle = "User is not a member nor creator of the project.";
            Assert.Equal(expectedTitle, await ActualTitle(response));

        }
    }
}
