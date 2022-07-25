using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Noctools.Infrastructure.RestClient.Client;
using Noctools.Infrastructure.RestClient.Exceptions;
using Noctools.Infrastructure.RestClient.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Noctools.Infrastructure.RestClient
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpPolly<TRestClient>(this IServiceCollection services,
            IConfiguration configuration)
            where TRestClient : class, IRestClient
        {
            var clientConfigSection = configuration.GetSection("RestClient");
            services.Configure<RestClientOptions>(clientConfigSection);
            var clientConfig = clientConfigSection.Get<RestClientOptions>();

            if (clientConfig == null)
                throw new ClientConfigurationCouldNotBeNullException();

            var socketHandler = new SocketsHttpHandler
            {
                MaxConnectionsPerServer = int.MaxValue,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                PooledConnectionLifetime = Timeout.InfiniteTimeSpan,
                ConnectTimeout = clientConfig.ConnectTimeout,
                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(2),
                ResponseDrainTimeout = TimeSpan.FromSeconds(2),
                Expect100ContinueTimeout = TimeSpan.FromSeconds(1),
            };

            IHttpClientBuilder httpClientBuilder = services.AddHttpClient<IRestClient, TRestClient>()
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
                .AddPolicyHandler(GetTimeoutPolicy(clientConfig.Timeout, clientConfig.TimeoutStrategy))
                .AddPolicyHandler(GetRetryPolicy(clientConfig.PolicyOptions.HttpRetry.Count,
                    clientConfig.PolicyOptions.HttpRetry.BackoffPower))
                .AddPolicyHandler(GetCircuitBreakerPolicy(
                    clientConfig.PolicyOptions.HttpCircuitBreaker.ExceptionsAllowedBeforeBreaking,
                    clientConfig.PolicyOptions.HttpCircuitBreaker.DurationOfBreak))
                .ConfigurePrimaryHttpMessageHandler(() => socketHandler);

            if (clientConfig.PolicyOptions != null && clientConfig.PolicyOptions.Fallback != null &&
                clientConfig.PolicyOptions.Fallback.Enabled)
            {
                Debug.WriteLine("Fallback Policy Aktif Edildi.");
                httpClientBuilder.AddPolicyHandler(GetDefaultValueFallbackPolicy());
            }

            return services;
        }

        public static IServiceCollection AddTypedHttpPolly<TServiceProxyInterface, TServiceProxyImplementation>(
            this IServiceCollection services,
            RestClientOptions clientConfig) where TServiceProxyInterface : class
            where TServiceProxyImplementation : class, TServiceProxyInterface
        {
            if (clientConfig == null)
                throw new ClientConfigurationCouldNotBeNullException();

            var socketHandler = new SocketsHttpHandler
            {
                MaxConnectionsPerServer = int.MaxValue,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                PooledConnectionLifetime = Timeout.InfiniteTimeSpan,
                ConnectTimeout = clientConfig.ConnectTimeout,
                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(2),
                ResponseDrainTimeout = TimeSpan.FromSeconds(2),
                Expect100ContinueTimeout = TimeSpan.FromSeconds(1),
            };
            IHttpClientBuilder httpClientBuilder = services
                .AddHttpClient<TServiceProxyInterface, TServiceProxyImplementation>()
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan)
                .AddPolicyHandler(GetTimeoutPolicy(clientConfig.Timeout, clientConfig.TimeoutStrategy))
                .AddPolicyHandler(GetRetryPolicy(clientConfig.PolicyOptions.HttpRetry.Count,
                    clientConfig.PolicyOptions.HttpRetry.BackoffPower))
                .AddPolicyHandler(GetCircuitBreakerPolicy(
                    clientConfig.PolicyOptions.HttpCircuitBreaker.ExceptionsAllowedBeforeBreaking,
                    clientConfig.PolicyOptions.HttpCircuitBreaker.DurationOfBreak))
                .ConfigurePrimaryHttpMessageHandler(() => socketHandler);


            if (clientConfig.PolicyOptions != null && clientConfig.PolicyOptions.Fallback != null &&
                clientConfig.PolicyOptions.Fallback.Enabled)
            {
                Debug.WriteLine("Fallback Policy Aktif Edildi.");
                httpClientBuilder.AddPolicyHandler(GetDefaultValueFallbackPolicy());
            }

            return services;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount, int backoffPower)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<BrokenCircuitException>()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.ServiceUnavailable)
                .WaitAndRetryAsync(retryCount,
                    retryAttempt =>
                        TimeSpan.FromMilliseconds(Math.Pow(
                            backoffPower,
                            retryAttempt)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy(TimeSpan timeout,
            TimeoutStrategy timeoutStrategy) =>
            Policy.TimeoutAsync<HttpResponseMessage>(timeout,
                timeoutStrategy);

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(
            int exceptionsAllowedBeforeBreaking,
            TimeSpan durationOfBreak)
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking,
                    durationOfBreak);
        }

        private static IAsyncPolicy<HttpResponseMessage> GetDefaultValueFallbackPolicy()
        {
            return Policy<HttpResponseMessage>
                .Handle<Exception>()
                .FallbackAsync(new HttpResponseMessage());
        }
    }
}