using Mapster;
using MapsterMapper;

namespace Api.Common.Mappings
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            TypeAdapterConfig cfg = TypeAdapterConfig.GlobalSettings;
            cfg.Scan(typeof(DependencyInjection).Assembly);

            services.AddSingleton(cfg);
            services.AddScoped<IMapper, Mapper>();
            return services;
        }
    }
}
