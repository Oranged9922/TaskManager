using Api;
using Contracts.User.CreateUser;
using Contracts.User.LoginUser;
using IntegrationTests.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.UserTests
{
    public class LoginUserTests(WebApplicationFactory<Program> factory) : IntegrationTestBase(factory)
    {

        [Fact]
        public async Task User_LoginUser_DummyUser()
        {
            await CreateNewUser(Name: "test", Password: "P@ssw0rd!", Email: "test@test.test");

            LoginUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd!");

            var response = await Client.PostAsJsonAsync(Endpoint.UserController.LoginUser, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            ExpectedErrorsList? expectedErrors = [];
            Assert.True(await HasExpectedErrors(response, expectedErrors));
        }

        [Fact]
        public async Task User_LoginUser_InvalidCredentials()
        {
            LoginUserRequest request = new(
                Username: "aAaCa\\aa",
                Password: "AaSsd3.23)!");

            var response = await Client.PostAsJsonAsync(Endpoint.UserController.LoginUser, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            ExpectedErrorsList? expectedErrors = [("703",["Invalid credentials."])];
            Assert.True(await HasExpectedErrors(response, expectedErrors));
        }
    }
}