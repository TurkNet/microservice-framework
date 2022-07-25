using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Noctools.Infrastructure.RestClient.Client
{
    public class RestClient : IRestClient
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public RestClient(HttpClient httpClient, ILogger<RestClient> logger)
        {
            _client = httpClient;
            _logger = logger;
        }

        public async Task<TResponse> GetAsync<TResponse>(string serviceUrl,
            CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null, int[] acceptedHttpStatus = null)
        {
            _logger.LogInformation("External Service Get Request Starting Url: {serviceUrl} ", serviceUrl);

            HttpRequestMessage httpRequestMessage = CreateRequestMessage(HttpMethod.Get, serviceUrl, headers);

            return await SendAsync<TResponse>(httpRequestMessage, cancellationToken, acceptedHttpStatus);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string serviceUrl, TRequest request,
            CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null,
            int[] acceptedHttpStatus = null)
        {
            _logger.LogInformation("External Service Post Request Starting Url: {serviceUrl} ", serviceUrl);


            HttpRequestMessage httpRequestMessage = CreateRequestMessage(HttpMethod.Post, serviceUrl, request, headers);

            return await SendAsync<TResponse>(httpRequestMessage, cancellationToken, acceptedHttpStatus);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string serviceUrl, TRequest request,
            CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null,
            int[] acceptedHttpStatus = null)
        {
            _logger.LogInformation("External Service Put Request Starting Url: {serviceUrl} ", serviceUrl);

            HttpRequestMessage httpRequestMessage = CreateRequestMessage(HttpMethod.Put, serviceUrl, request, headers);

            return await SendAsync<TResponse>(httpRequestMessage, cancellationToken, acceptedHttpStatus);
        }

        public async Task<bool> DeleteAsync(string serviceUrl,
            CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null,
            int[] acceptedHttpStatus = null)
        {
            _logger.LogInformation("External Service Delete Request Starting Url: {serviceUrl} ", serviceUrl);

            bool result;

            HttpRequestMessage httpRequestMessage = CreateRequestMessage(HttpMethod.Delete, serviceUrl, headers);

            var response = await _client.SendAsync(httpRequestMessage, cancellationToken);

            if (acceptedHttpStatus == null || !acceptedHttpStatus.Contains((int) response.StatusCode))
                response.EnsureSuccessStatusCode();

            if ((int) response.StatusCode >= 500 || !ResponseMediaTypeIsValid(response))
            {
                result = false;
            }
            else
                result = true;


            return await Task.FromResult(result);
        }

        public async Task<TResponse> DeleteAsync<TResponse>(string serviceUrl,
            CancellationToken cancellationToken,
            ConcurrentDictionary<string, string> headers = null,
            int[] acceptedHttpStatus = null)
        {
            _logger.LogInformation("External Service Delete Request Starting Url: {serviceUrl} ", serviceUrl);

            HttpRequestMessage httpRequestMessage = CreateRequestMessage(HttpMethod.Delete, serviceUrl, headers);

            return await SendAsync<TResponse>(httpRequestMessage, cancellationToken, acceptedHttpStatus);
        }

        #region Private Method(s)

        private async Task<TResponse> ResponseMessageToObjectAsync<TResponse>(HttpResponseMessage responseMessage)
        {
            if (responseMessage == null || responseMessage.Content == null)
                return await Task.FromResult(default(TResponse));

            if (!ResponseMediaTypeIsValid(responseMessage))
                return await Task.FromResult(default(TResponse));

            TResponse result = default(TResponse);

            string responseBody = responseMessage.IsSuccessStatusCode
                ? await responseMessage.Content.ReadAsStringAsync()
                : responseMessage.Content != null
                    ? await responseMessage.Content.ReadAsStringAsync()
                    : string.Empty;


            if (!string.IsNullOrEmpty(responseBody))
                result = JsonConvert.DeserializeObject<TResponse>(responseBody);

            return await Task.FromResult(result);
        }

        private static StringContent GetStringContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, "application/json");
        }

        private bool ResponseMediaTypeIsValid(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return true;
            return response.Content.Headers.ContentType.MediaType == "application/json";
        }

        private async Task<TResponse> SendAsync<TResponse>(HttpRequestMessage httpRequestMessage,
            CancellationToken cancellationToken, int[] acceptedHttpStatus = null)
        {
            var response = await _client.SendAsync(httpRequestMessage, cancellationToken);

            if (response == null || response.Content == null)
                return await Task.FromResult(default(TResponse));

            if (acceptedHttpStatus == null || !acceptedHttpStatus.Contains((int) response.StatusCode))
                response.EnsureSuccessStatusCode();

            return await ResponseMessageToObjectAsync<TResponse>(response);
        }

        private HttpRequestMessage CreateRequestMessage(HttpMethod method, string serviceUrl,
            ConcurrentDictionary<string, string> headers = null)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, serviceUrl);
            httpRequestMessage.Headers.Accept.Clear();
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (headers != null && headers.Any())
                foreach (var header in headers)
                {
                    httpRequestMessage.Headers.Add(header.Key, header.Value);
                }

            return httpRequestMessage;
        }

        private HttpRequestMessage CreateRequestMessage<TRequest>(HttpMethod method, string serviceUrl,
            TRequest request, ConcurrentDictionary<string, string> headers = null)
        {
            HttpRequestMessage httpRequestMessage = CreateRequestMessage(method, serviceUrl, headers);

            httpRequestMessage.Content = GetStringContent(JsonConvert.SerializeObject(request));

            return httpRequestMessage;
        }

        #endregion
    }
}