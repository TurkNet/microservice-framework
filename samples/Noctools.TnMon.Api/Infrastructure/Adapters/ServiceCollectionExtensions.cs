using Microsoft.Extensions.DependencyInjection;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Infrastructure.Adapters
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdapters(this IServiceCollection services)
        {
            services.AddScoped<IMailAdapter, MailAdapter>();
            services.AddScoped<IOpenGearAdapter, OpenGearAdapter>();
            services.AddScoped<IThresholdAdapter, ThresholdAdapter>();
            return services;
        }
    }
}