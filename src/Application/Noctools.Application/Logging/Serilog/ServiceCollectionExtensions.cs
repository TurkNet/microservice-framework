using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Noctools.Application.Logging.Serilog.Enrichers.HttpContext;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;


namespace Noctools.Application.Logging.Serilog
{
    /// <summary>
    /// todo: it should be move on infrastructure
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// for stdout on cloud
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseLogger(
            this IWebHostBuilder builder)
        {
            return builder.ConfigureLogging((context, logging) => { logging.ClearProviders(); })
                .UseSerilog((provider, context, configuration) =>
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithEnvironmentUserName()
                        .Enrich.WithProcessId()
                        .Enrich.WithProcessName()
                        .Enrich.WithThreadId()
                        .Enrich.WithThreadName()
                        .Enrich.WithHttpContext(provider)
                        .WriteTo.Async(
                            s => s.Console(new ElasticsearchJsonFormatter()
                            ));
                });
        }

        /// <summary>
        /// for push to elk
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseLogger(
            this IWebHostBuilder builder, string appName)
        {
            return builder.ConfigureLogging((context, logging) => { logging.ClearProviders(); })
                .UseSerilog((provider, context, configuration) =>
                {
                    configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithEnvironmentUserName()
                        .Enrich.WithProcessId()
                        .Enrich.WithProcessName()
                        .Enrich.WithThreadId()
                        .Enrich.WithThreadName()
                        .Enrich.WithHttpContext(provider)
                        .WriteTo.Async(
                            s => s.Console(new ElasticsearchJsonFormatter()
                            ))
                        .WriteTo.Elasticsearch(
                            new ElasticsearchSinkOptions(
                                new Uri(context.Configuration["Logging:Elasticsearch:Uri"]))
                            {
                                MinimumLogEventLevel = LogEventLevel.Debug,
                                IndexFormat = $"{appName}-{{0:yyyy.MM.dd}}"
                            });
                });
        }
    }
}