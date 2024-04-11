using Manyminds.Application.AutoMappers;
using Microsoft.Extensions.DependencyInjection;

namespace Manyminds.Application
{
    public static class AutoMapperDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ConfigurationMapping));
            return services;
        }
    }
}
