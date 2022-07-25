using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Noctools.Bus;
using Noctools.Domain;

namespace Noctools.Bus
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainEventBus(this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Scoped<IDomainEventDispatcher, DomainEventDispatcher>());
            return services;
        }
    }
}