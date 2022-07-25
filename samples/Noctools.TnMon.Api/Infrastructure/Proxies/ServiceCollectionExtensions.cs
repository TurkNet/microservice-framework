using Microsoft.Extensions.DependencyInjection;
using Noctools.TnMon.Api.Controllers;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Infrastructure.Proxies
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProxies(this IServiceCollection services)
        {
            services.AddScoped<IConfigurationProxy, ConfigurationProxy>();
            services.AddScoped<ITicketProxy, TicketProxy>();
            services.AddScoped<IProductProxy, ProductProxy>();
            return services;
        }
    }
}
