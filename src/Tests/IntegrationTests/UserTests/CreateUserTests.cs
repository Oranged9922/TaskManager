using Api;
using Contracts.User.CreateUser;
using IntegrationTests.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests.UserTests
{
    public class CreateUserTests(WebApplicationFactory<Program> factory) : IntegrationTestBase(factory)
    {
        private static readonly string _endpoint = "/user/create";

        [Fact(Skip = "Template test")]
        public async Task TemplateTest()
        {
            CreateUserRequest request = new(
               Username: "test",
               Password: "P@ssw0rd!",
               Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            ExpectedErrorsList? expectedErrors = null;
            Assert.True(await HasExpectedErrors(response, expectedErrors));
        }

        [Fact]
        public async Task User_CreateUser()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd!",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            ExpectedErrorsList? expectedErrors = null;
            Assert.True(await HasExpectedErrors(response, expectedErrors));
        }

        [Fact]
        public async Task User_CreateUser_InvalidEmail()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd!",
                Email: "test");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            ExpectedErrorsList expectedErrors = [
                ("Email", ["Your e-mail address is not valid."])
                ];

            Assert.True(await HasExpectedErrors(response, expectedErrors));

        }

        [Fact]
        public async Task User_CreateUser_InvalidPassword()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "password",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            ExpectedErrorsList expectedErrors = [
                ("Password",
                    [
                        "Your password must contain at least one uppercase letter.",
                        "Your password must contain at least one number.",
                        "Your password must contain at least one (!? *.)."
                    ])
                ];

            Assert.True(await HasExpectedErrors(response, expectedErrors));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task User_CreateUser_InvalidUsername()
        {
            CreateUserRequest request = new(
                Username: "t",
                Password: "P@ssw0rd!",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            ExpectedErrorsList expectedErrors = [
                ("Username",
                    [
                        "Your username length must be at least 3 symbols long."
                    ])
                ];

            Assert.True(await HasExpectedErrors(response, expectedErrors));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async Task User_CreateUser_UsernameAlreadyExists()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd!",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);

            var expectedTitle = "User with given username already exists in the database.";

            Assert.True(await HasExpectedTitle(response, expectedTitle));
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);

        }

        [Fact]
        public async Task User_CreateUser_EmailAlreadyExists()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd!",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            request = new(
                Username: "test2",
                Password: "P@ssw0rd!",
                Email: "test@test.eu");

            response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            var expectedTitle = "User with given email already exists in the database.";

            Assert.True(await HasExpectedTitle(response, expectedTitle));
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);


        }
    }
}
