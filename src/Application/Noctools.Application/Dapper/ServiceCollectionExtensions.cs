using Microsoft.Extensions.DependencyInjection;

namespace Noctools.Application.Dapper
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperPolly(this IServiceCollection services)
        {
            services.AddSingleton<IDapperPolly, DapperPolly>();
            return services;
        }
    }
}
