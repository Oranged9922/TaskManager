using Application.Common.Interfaces;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
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
            services.AddScoped<ITOTaskRepository, TOTaskRepository>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            //services.AddScoped<ITaskService, TaskService>();
            return services;
        }
    }
}
