using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Core;
using Serilog.Formatting.Elasticsearch;

namespace Noctools.Application.Logging.Serilog.Extensions
{
    public static class WebhostBuilderExtensions
    {
        public static IWebHostBuilder UseLogger(
            this IWebHostBuilder builder)
        {
            return builder
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
                        .WriteTo.Async(s => s.Console(new ElasticsearchJsonFormatter()))
                        ;
                });
        }

        public static IHostBuilder UseLogger(
            this IHostBuilder builder)
        {
            return builder
                .UseSerilog((services, context, configuration) =>
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
                        .WriteTo.Async(s => s.Console(new ElasticsearchJsonFormatter()))
                        ;
                });
        }

        /// <summary>Sets Serilog as the logging provider.</summary>
        /// <remarks>
        /// A <see cref="T:Microsoft.AspNetCore.Hosting.WebHostBuilderContext" /> is supplied so that configuration and hosting information can be used.
        /// The logger will be shut down when application services are disposed.
        /// </remarks>
        /// <param name="builder">The web host builder to configure.</param>
        /// <param name="configureLogger">The delegate for configuring the <see cref="T:Serilog.LoggerConfiguration" /> that will be used to construct a <see cref="T:Microsoft.Extensions.Logging.Logger" />.</param>
        /// <param name="preserveStaticLogger">Indicates whether to preserve the value of <see cref="P:Serilog.Log.Logger" />.</param>
        /// <returns>The web host builder.</returns>
        private static IWebHostBuilder UseSerilog(this IWebHostBuilder builder,
            Action<IServiceProvider, WebHostBuilderContext, LoggerConfiguration> configureLogger,
            bool preserveStaticLogger = false)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (configureLogger == null)
                throw new ArgumentNullException(nameof(configureLogger));

            builder.ConfigureServices((context, collection) =>
            {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
                configureLogger(collection.BuildServiceProvider(), context, loggerConfiguration);
                Logger logger = loggerConfiguration.CreateLogger();
                if (preserveStaticLogger)
                {
                    collection.AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(logger, true));
                }
                else
                {
                    Log.Logger = logger;
                    collection.AddSingleton(services => (ILoggerFactory)new SerilogLoggerFactory(null, true));
                }
            });
            return builder;
        }

        private static IHostBuilder UseSerilog(this IHostBuilder builder,
            Action<IServiceCollection, HostBuilderContext, LoggerConfiguration> configureLogger,
            bool preserveStaticLogger = false)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            if (configureLogger == null)
                throw new ArgumentNullException(nameof(configureLogger));

            builder.ConfigureServices((context, collection) =>
            {
                LoggerConfiguration loggerConfiguration = new LoggerConfiguration();

                configureLogger(collection, context, loggerConfiguration);
                Logger logger = loggerConfiguration.CreateLogger();
                collection.AddLogging(b =>
                {
                    b.AddSerilog(logger: logger, dispose: true);
                });
            });
            return builder;
        }
    }
}
