using Noctools.Infrastructure.RestClient.Client;

namespace Noctools.Infrastructure.RestClient.Proxy
{
    public abstract class ProxyServiceBase
    {
        protected readonly Client.RestClient RestClient;

        protected ProxyServiceBase(Client.RestClient rest)
        {
            RestClient = rest;
        }
    }
}