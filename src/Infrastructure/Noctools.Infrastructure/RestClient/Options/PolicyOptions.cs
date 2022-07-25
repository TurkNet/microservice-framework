namespace Noctools.Infrastructure.RestClient.Options
{
    public class PolicyOptions
    {
        public CircuitBreakerPolicyOptions HttpCircuitBreaker { get; set; }
        public RetryPolicyOptions HttpRetry { get; set; }
        public FallbackPolicyOptions Fallback { get; set; }
    }
}