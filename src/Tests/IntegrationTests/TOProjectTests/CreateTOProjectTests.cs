using Api;
using Application.TOProjectLogic.Commands.CreateTOProject;
using Contracts.TOProject.CreateTOProject;
using IntegrationTests.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.TOProjectTests
{
    public class CreateTOProjectTests(WebApplicationFactory<Program> factory) : IntegrationTestBase(factory)
    {
        [Fact]
        public async Task TOProject_Create()
        {
            var jwtToken = await CreateAndLoginTestUser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            CreateTOProjectRequest request = new(
                Name: "test",
                Description: "test");

            var response = await Client.PostAsJsonAsync(Endpoint.TOProjectController.CreateProject, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            ExpectedErrorsList? expectedErrors = [];
            Assert.True(await HasExpectedErrors(response, expectedErrors));

            var result = await response.Content.ReadFromJsonAsync<CreateTOProjectCommandResponse>();
            Assert.NotNull(result?.Id);
            Assert.Equal(request.Name, result?.Name);
        }

        [Fact]
        public async Task TOProject_Create_DuplicateName()
        {
            var jwtToken = await CreateAndLoginTestUser();
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);

            CreateTOProjectRequest request = new(
                Name: "test",
                Description: "test");

            _ = await Client.PostAsJsonAsync(Endpoint.TOProjectController.CreateProject, request);
            var response = await Client.PostAsJsonAsync(Endpoint.TOProjectController.CreateProject, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

            var expectedTitle = "Project with given name already exists in the database.";

            Assert.True(await HasExpectedTitle(response, expectedTitle));
        }
    }
}
