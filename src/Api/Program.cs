using Application;
using Infrastructure;
using Infrastructure.Persistance;
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

        WebApplication app = builder.Build();


        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();
        app.MapControllers();

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TaskOrganizerDbContext>();
        context.Database.EnsureCreated();
        context.Database.Migrate();

        app.Run();
    }
}