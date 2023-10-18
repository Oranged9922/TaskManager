using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
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

        public void ClearDatabase(bool delete = false)
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TaskOrganizerDbContext>();

            if (delete)
            {
                context.Database.EnsureDeleted();
                return;
            }

            var dbSets = context.GetType().GetProperties()
                .Where(p => p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .ToList();

            foreach (var dbSetProperty in dbSets)
            {
                dynamic dbSet = dbSetProperty.GetValue(context)!;
                context.RemoveRange(dbSet);
            }

            context.SaveChanges();
        }

        public void Dispose()
        {
            ClearDatabase(delete : true);
        }

        public static async Task<bool> HasExpectedErrors(HttpResponseMessage response, List<(string Code, List<string> Descriptions)>? expected)
        {
            if (expected is null || expected.Count == 0) { return true; }

            string content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content)!;
            if (problemDetails.Extensions.TryGetValue("errors", out object? errors))
            {
                var _errors = CreateListFromObject(errors);
                return HasExpectedErrors(_errors, expected);
            }

            return false;
        }

        public static async Task<bool> HasExpectedTitle(HttpResponseMessage response, string? title)
        {
            string content = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content)!;
            if (problemDetails.Extensions.TryGetValue("title", out object? _title))
            {
                return _title?.ToString() == title;
            }
            return title is null;
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

    }
}