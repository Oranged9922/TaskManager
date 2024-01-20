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
            Assert.True(await HasExpectedErrors(response, expectedErrors));

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
            Assert.True(await HasExpectedErrors(response, expectedErrors));

            var result = await response.Content.ReadFromJsonAsync<CreateTOTaskCommandResponse>();
            Assert.NotNull(result?.Id);
            Assert.Equal(request.Name, result?.Name);
        }
    }
}
