using Microsoft.AspNetCore.Mvc.Infrastructure;
using TaskOrganizer.Api.Common.Errors;

namespace TaskOrganizer.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, TaskOrganizerProblemDetailsFactory>();
            return services;
        }
    }
}
