using System;

namespace Noctools.Infrastructure.RestClient.Options
{
    public class CircuitBreakerPolicyOptions
    {
        public int ExceptionsAllowedBeforeBreaking { get; set; }
        public TimeSpan DurationOfBreak { get; set; }
    }
}