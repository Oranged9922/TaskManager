using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Common.Behaviors;
using Microsoft.AspNetCore.Http;
using Application.Services.AuthorizationService;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
