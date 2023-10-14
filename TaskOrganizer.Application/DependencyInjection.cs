using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskOrganizer.Application.Common.Behaviors;
using System.Reflection;

namespace TaskOrganizer.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}
