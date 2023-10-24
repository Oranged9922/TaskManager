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
        private static readonly string _endpoint = "/user/login";

        private async Task RegisterDummyUser()
        {
            CreateUserRequest request = new(
               Username: "test",
               Password: "P@ssw0rd!",
               Email: "test@test.eu");

            await Client.PostAsJsonAsync("/user/create", request);
        }

        [Fact]
        public async Task User_LoginUser_DummyUser()
        {
            await RegisterDummyUser();
            LoginUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd!");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            ExpectedErrorsList? expectedErrors = null;
            Assert.True(await HasExpectedErrors(response, expectedErrors));
        }
    }
}