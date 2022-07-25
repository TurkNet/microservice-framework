using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Configuration;

namespace Noctools.Application.Logging.Serilog.Enrichers.HttpContext
{
    /// <summary>
    /// Extends <see cref="LoggerConfiguration"/> to add enrichers for <see cref="Microsoft.AspNetCore.Http.HttpContext"/>.
    /// capabilities.
    /// </summary>
    public static class LoggerEnrichmentConfigurationExtensions
    {
        /// <summary>
        /// Enrich log events with Aspnetcore httpContext properties.
        /// </summary>
        /// <param name="enrichmentConfiguration">Logger enrichment configuration.</param>
        /// <param name="serviceProvider"></param>
        /// <returns>Configuration object allowing method chaining.</returns>
        public static LoggerConfiguration WithHttpContext(this LoggerEnrichmentConfiguration enrichmentConfiguration,
            IServiceProvider serviceProvider, Func<IHttpContextAccessor, object> customMethod = null)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));

            var enricher = serviceProvider.GetService<HttpContextEnricher>();

            if (customMethod != null)
                enricher.SetCustomAction(customMethod);

            return enrichmentConfiguration.With(enricher);
        }
    }
}
