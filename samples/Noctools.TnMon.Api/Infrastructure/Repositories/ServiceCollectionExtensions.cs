using Microsoft.Extensions.DependencyInjection;
using Noctools.TnMon.Api.Controllers;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Infrastructure.Repositories
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILogQueryRepository, LogQueryRepository>();
            services.AddScoped<ILogCommandRepository, LogCommandRepository>();
            services.AddScoped<INocInformationQueryRepository, NocInformationQueryRepository>();
            services.AddScoped<INocInformationCommandRepository, NocInformationCommandRepository>();
            return services;
        }
    }
}
