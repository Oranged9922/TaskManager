using Application;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
namespace Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddPresentation()
            .AddApplication()
            .AddInfrastructure(builder.Configuration);
        builder.Services.AddCors();
        builder.Services.AddControllers();

        WebApplication app = builder.Build();
        {

            app.UseExceptionHandler("/error");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<TaskOrganizerDbContext>();
            context.Database.Migrate();

            app.Run();
        }
    }
}