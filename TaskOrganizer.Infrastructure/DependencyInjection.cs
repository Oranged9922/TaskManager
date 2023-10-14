using Microsoft.Extensions.DependencyInjection;
using TaskOrganizer.Infrastructure.Persistance;

namespace TaskOrganizer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<TaskOrganizerDbContext>();
            services.AddPersistence();
            services.AddServices();
            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            //services.AddScoped<ITaskRepository, TaskRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
