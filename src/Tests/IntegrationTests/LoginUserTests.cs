using Contracts.User.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Api;
using Contracts.User.LoginUser;

namespace IntegrationTests
{
    public class LoginUserTests(WebApplicationFactory<Program> factory) : ApiTests(factory)
    {
        private static readonly string _endpoint = "/user/login";

        public async Task RegisterDummyUser()
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
            RegisterDummyUser().GetAwaiter().GetResult();
            LoginUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd!");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK ,response.StatusCode);

            ExpectedErrorsList? expectedErrors = null;
            Assert.True(await HasExpectedErrors(response, expectedErrors));
        }
    }
}