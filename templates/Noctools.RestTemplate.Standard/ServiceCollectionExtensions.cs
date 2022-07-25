using System;
using BeatPulse.Core;
using MessagePack.AspNetCoreMvcFormatter;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Noctools.Application.CleanArch;
using Noctools.Application.Microservice;
using Noctools.Application.OpenApi;
using Noctools.Domain;
using Noctools.Infrastructure;
using Noctools.Infrastructure.Features;

namespace Noctools.RestTemplate.Standard
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddStandardTemplate(this IServiceCollection services,
            Action<IServiceCollection, IServiceProvider> preHook = null,
            Action<BeatPulseContext> beatPulseCtx = null)
        {
            services.AddFeatureToggle();

            using (var scope = services.BuildServiceProvider().GetService<IServiceScopeFactory>().CreateScope())
            {
                var svcProvider = scope.ServiceProvider;
                var config = svcProvider.GetRequiredService<IConfiguration>();
                var env = svcProvider.GetRequiredService<IHostingEnvironment>();
                var feature = svcProvider.GetRequiredService<IFeature>();

                preHook?.Invoke(services, svcProvider);

                if (feature.IsEnabled("Rest"))
                    services.AddRestClientCore(config);

                services.AddSingleton<IDomainEventDispatcher, MemoryDomainEventDispatcher>();

                services.AddAutoMapperCore(config.LoadFullAssemblies());
                services.AddMediatRCore(config.LoadFullAssemblies());

                if (feature.IsEnabled("CleanArch"))
                    services.AddCleanArch();

                services.AddCacheCore();

                if (feature.IsEnabled("ApiVersion"))
                    services.AddApiVersionCore(config);

                var mvcBuilder = services.AddMvcCore(config);

                if (feature.IsEnabled("MessagePack"))
                    mvcBuilder.AddMvcOptions(option =>
                    {
                        option.OutputFormatters.Clear();
                        option.OutputFormatters.Add(
                            new MessagePackOutputFormatter(ContractlessStandardResolver.Options));
                        option.InputFormatters.Clear();
                        option.InputFormatters.Add(
                            new MessagePackInputFormatter(ContractlessStandardResolver.Options));
                    });

                services.AddDetailExceptionCore();

                if (feature.IsEnabled("AuthN"))
                    services.AddAuthNCore(config, env);

                if (feature.IsEnabled("OpenApi"))
                    services.AddOpenApiCore(config, feature);

                services.AddCorsCore();

                services.AddHeaderForwardCore(env);

                if (feature.IsEnabled("OpenApi:Profiler"))
                    services.AddApiProfilerCore();

                services.AddBeatPulse(beatPulseCtx);

                if (feature.IsEnabled("ResponseCompression"))
                    services.AddResponseCompression();
            }

            return services;
        }
    }
}