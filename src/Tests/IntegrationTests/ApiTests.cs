using Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
    }

}
