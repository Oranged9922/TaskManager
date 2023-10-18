using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Behaviors;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using System.Reflection;
using Application.UserLogic.Commands.CreateUser;
using Contracts.User.CreateUser;
using FluentValidation.AspNetCore;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));


            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

            return services;
        }
    }
}
