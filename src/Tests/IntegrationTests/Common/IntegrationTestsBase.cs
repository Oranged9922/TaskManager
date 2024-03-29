﻿using Application.UserLogic.Queries.LoginUser;
using Contracts.User.CreateUser;
using Contracts.User.LoginUser;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntegrationTests.Common
{
    public class IntegrationTestBase : IClassFixture<WebApplicationFactory<Api.Program>>
    {
        protected readonly WebApplicationFactory<Api.Program> _factory;
        public HttpClient Client { get; init; }
        public IntegrationTestBase(WebApplicationFactory<Api.Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the app's DbContext registration
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<TaskOrganizerDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Add SQLite DbContext using an in-memory database for testing
                    services.AddSingleton<PublishDomainEventsInterceptor>(); // Assuming registered the interceptor

                    services.AddSingleton(sp =>
                    {
                        var interceptor = sp.GetRequiredService<PublishDomainEventsInterceptor>();
                        var uniqueDbName = Guid.NewGuid().ToString();
                        return new TaskOrganizerDbContext(interceptor, true, uniqueDbName);
                    });


                    // Build the service provider
                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<TaskOrganizerDbContext>();

                    // Ensure the database is created
                    db.Database.EnsureCreated();
                });
            });
            Client = _factory.CreateClient();
        }

        public static async Task<List<(string, List<string>)>> ActualErrors(HttpResponseMessage response)
        {

            string content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content)!;
            if (problemDetails.Extensions.TryGetValue("errors", out object? errors))
            {
                var _errors = CreateListFromObject(errors);
                return _errors;
            }
            return [];
        }

        public static async Task<string?> ActualTitle(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content)!;
            if (problemDetails.Extensions.TryGetValue("title", out object? _title))
            {
                return _title?.ToString();
            }
            return null;
        }

        private static bool HasExpectedErrors(List<(string code, List<string> descriptions)>? response, List<(string code, List<string> descriptions)>? errors)
        {
            if (response is null || response.Count == 0) return false;
            if (errors is null || errors.Count == 0) return false;
            if (response.Count != errors.Count)
            {
                return false;
            }

            foreach (var (code, descriptions) in errors)
            {
                if (!response.Any(r => r.code == code && r.descriptions.SequenceEqual(descriptions)))
                {
                    return false;
                }
            }

            return true;
        }

        public static List<(string, List<string>)> CreateListFromObject(object? errors)
        {
            var list = new List<(string, List<string>)>();
            if (errors is JsonElement element && element.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty prop in element.EnumerateObject())
                {
                    string code = prop.Name;
                    List<string> descriptions = [];
                    if (prop.Value.ValueKind == JsonValueKind.Array)
                    {
                        foreach (JsonElement descElement in prop.Value.EnumerateArray())
                        {
                            descriptions.Add(descElement.GetString() ?? "");
                        }
                    }
                    list.Add((code, descriptions));
                }
            }
            return list;
        }

        public async Task<string> CreateNewUser(string Name = "test", string Password = "P@ssw0rd!", string Email = "test@test.test")
        {
            CreateUserRequest request = new(
                Username: Name,
                Password: Password,
                Email: Email);

            var response = await Client.PostAsJsonAsync(Endpoint.UserController.CreateUser, request);
            var token = await response.ExtractAndParseJwtTokenAsync();
            return token.UnsafeToString();
        }

        public async Task<string> CreateAndLoginTestUser(string Name = "test", string Password = "P@ssw0rd!", string Email = "test@test.test")
        {
            CreateUserRequest request = new(
                Username: Name,
                Password: Password,
                Email: Email);

            _ = await Client.PostAsJsonAsync(Endpoint.UserController.CreateUser, request);

            LoginUserRequest loginRequest = new(
                Username: Name,
                Password: Password);

            var response = await Client.PostAsJsonAsync(Endpoint.UserController.LoginUser, loginRequest);
            var token = await response.ExtractAndParseJwtTokenAsync();
            return token.UnsafeToString();
        }
    }
}
