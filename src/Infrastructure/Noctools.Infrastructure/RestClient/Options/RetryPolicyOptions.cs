namespace Noctools.Infrastructure.RestClient.Options
{
    public class RetryPolicyOptions
    {
        public int BackoffPower { get; set; }
        public int Count { get; set; }
    }
}