using Api;
using Contracts.User.CreateUser;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace IntegrationTests
{
    public class CreateUserTests(WebApplicationFactory<Program> factory) : ApiTests(factory)
    {
        private static readonly string _endpoint = "/user/create";

        [Fact]
        public async Task User_CreateUser()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        }

        [Fact]
        public async Task User_CreateUser_InvalidEmail()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd",
                Email: "test");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task User_CreateUser_InvalidUsername()
        {
            CreateUserRequest request = new(
                Username: "t",
                Password: "P@ssw0rd",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task User_CreateUser_UsernameAlreadyExists()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task User_CreateUser_EmailAlreadyExists()
        {
            CreateUserRequest request = new(
                Username: "test",
                Password: "P@ssw0rd",
                Email: "test@test.eu");

            var response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            request = new(
                Username: "test2",
                Password: "P@ssw0rd",
                Email: "test@test.eu");

            response = await Client.PostAsJsonAsync(_endpoint, request);
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
