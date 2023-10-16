﻿using ErrorOr;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace IntegrationTests
{
    public class ApiTests : IClassFixture<WebApplicationFactory<Api.Program>>, IDisposable
    {
        public readonly HttpClient Client;
        private readonly WebApplicationFactory<Api.Program> _factory;
        public ApiTests(WebApplicationFactory<Api.Program> factory)
        {
            Client = factory.CreateClient();
            _factory = factory;
            ClearDatabase();
        }

        public void ClearDatabase()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<TaskOrganizerDbContext>();

                context.Users.RemoveRange(context.Users);
                context.Tasks.RemoveRange(context.Tasks);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            ClearDatabase();
        }

        public async Task<bool> HasExpectedErrors(HttpResponseMessage response, List<(string Code, List<string> Descriptions)>? expected)
        {
            if (expected is null || expected.Count == 0) { return true; }
            
            string content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content)!;
            if (problemDetails.Extensions.TryGetValue("errors", out object? errors))
            {
                var _errors = CreateListFromObject(errors);
                return _HasExpectedErrors(_errors, expected);
            }

            return false;
        }

        public async Task<bool> HasExpectedTitle(HttpResponseMessage response, string? title)
        {
            string content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content)!;
            if (problemDetails.Extensions.TryGetValue("title", out object? _title))
            {
                return _title?.ToString() == title;
            }
            return title is null;
        }

        public bool _HasExpectedErrors(List<(string code, List<string> descriptions)>? response, List<(string code, List<string> descriptions)>? errors)
        {
            if (response is null || response.Count == 0) return false;
            if (errors is null || errors.Count == 0) return false;
            if (response.Count != errors.Count)
            {
                return false;
            }

            foreach (var error in errors)
            {
                if (!response.Any(r => r.code == error.code && r.descriptions.SequenceEqual(error.descriptions)))
                {
                    return false;
                }
            }

            return true;
        }

        public List<(string, List<string>)> CreateListFromObject(object? errors)
        {
            var list = new List<(string, List<string>)>();
            if (errors is JsonElement element && element.ValueKind == JsonValueKind.Object)
            {
                foreach (JsonProperty prop in element.EnumerateObject())
                {
                    string code = prop.Name;
                    List<string> descriptions = new List<string>();
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

    }
}