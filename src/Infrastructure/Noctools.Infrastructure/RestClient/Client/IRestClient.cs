using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Noctools.Infrastructure.RestClient.Client
{
    public interface IRestClient
    {
        Task<TResponse> GetAsync<TResponse>(string serviceUrl, CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null, int[] acceptedHttpStatus = null);

        Task<TResponse> PostAsync<TRequest, TResponse>(string serviceUrl, TRequest request,
            CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null, int[] acceptedHttpStatus = null);

        Task<TResponse> PutAsync<TRequest, TResponse>(string serviceUrl, TRequest request,
            CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null, int[] acceptedHttpStatus = null);

        Task<bool> DeleteAsync(string serviceUrl, CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null, int[] acceptedHttpStatus = null);

        Task<TResponse> DeleteAsync<TResponse>(string serviceUrl, CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null, int[] acceptedHttpStatus = null);
    }
}