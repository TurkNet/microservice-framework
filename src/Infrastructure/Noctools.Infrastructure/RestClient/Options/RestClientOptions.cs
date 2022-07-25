using System;
using Polly.Timeout;

namespace Noctools.Infrastructure.RestClient.Options
{
    public class RestClientOptions
    {
        public int MaxConnectionsPerServer { get; set; }
        public TimeSpan PooledConnectionLifetime { get; set; }
        public TimeSpan ConnectTimeout { get; set; }
        public TimeSpan PooledConnectionIdleTimeout { get; set; }
        public TimeSpan ResponseDrainTimeout { get; set; }
        public TimeSpan Expect100ContinueTimeout { get; set; }
        public TimeSpan HandlerLifeTime { get; set; }
        public TimeSpan Timeout { get; set; }
        public PolicyOptions PolicyOptions { get; set; }
        public TimeoutStrategy TimeoutStrategy { get; set; }
    }
}