using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Noctools.Infrastructure;
using Noctools.Infrastructure.Validation;
using Noctools.TnMon.Api.Infrastructure.Adapters;
using Noctools.TnMon.Api.Infrastructure.Proxies;
using Noctools.TnMon.Api.Infrastructure.Repositories;

namespace Noctools.TnMon.Api.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            using (var scope = services.BuildServiceProvider().GetService<IServiceScopeFactory>().CreateScope())
            {
                var svcProvider = scope.ServiceProvider;
                var config = svcProvider.GetRequiredService<IConfiguration>();
                services.AddProxies();
                services.AddRepositories();
                services.AddAdapters();
                services.AddValidationService(config.LoadFullAssemblies());
            }

            return services;
        }
    }
}