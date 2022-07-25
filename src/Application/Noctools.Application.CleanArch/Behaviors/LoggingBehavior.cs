using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Noctools.Application.CleanArch
{
    /// <summary>
    /// todo : move on infrastructure layer that's why clean arc is a feature flag
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var serializedRequest = JsonConvert.SerializeObject(request);
            _logger.LogInformation("Handling request with content @{serializedRequest}", serializedRequest);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
            {
                _logger.LogWarning($"[PERF] The request {typeof(TRequest).FullName} took {timeTaken.Seconds} seconds.");
            }
            
            // todo : ignore for list response
            // var serializedResponse = JsonConvert.SerializeObject(response);
            // _logger.LogInformation(
            //     "Handled response with content {@serializedResponse}", serializedResponse);

            return response;
        }
    }
}