using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Noctools.Application.Logging.Serilog.Enrichers.HttpContext
{
    public class HttpContextEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private Func<IHttpContextAccessor, object> _customAction = null;

        public HttpContextEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _customAction = StandardEnricher; // by default
        }


        public void SetCustomAction(Func<IHttpContextAccessor, object> customAction)
        {
            _customAction = customAction;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            Microsoft.AspNetCore.Http.HttpContext ctx = _httpContextAccessor.HttpContext;
            if (ctx == null) return;

            var httpContextCache = ctx.Items[$"serilog-enrichers-httpcontext"];

            if (httpContextCache == null)
            {
                httpContextCache = _customAction.Invoke(_httpContextAccessor);
                ctx.Items[$"serilog-enrichers-httpcontext"] = httpContextCache;
            }

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("HttpContext", httpContextCache, true));
        }

        public static object StandardEnricher(IHttpContextAccessor hca)
        {
            var ctx = hca.HttpContext;
            if (ctx == null) return null;

            var httpContextCache = new HttpContextCache
            {
                TraceIdentifier = ctx.TraceIdentifier,
                IpAddress = ctx.Connection.RemoteIpAddress.ToString(),
                Host = ctx.Request.Host.ToString(),
                Path = ctx.Request.Path.ToString(),
                IsHttps = ctx.Request.IsHttps,
                Scheme = ctx.Request.Scheme,
                Method = ctx.Request.Method,
                ContentType = ctx.Request.ContentType,
                Protocol = ctx.Request.Protocol,
                QueryString = ctx.Request.QueryString.ToString(),
                Query = ctx.Request.Query.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Headers = ctx.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()),
                Cookies = ctx.Request.Cookies.ToDictionary(x => x.Key, y => y.Value.ToString()),
                CorrelationId = ctx.Request.Headers != null && ctx.Request.Headers.ContainsKey("X-Correlation-Id")
                    ? ctx.Request.Headers["X-Correlation-Id"].ToString()
                    : Guid.NewGuid().ToString(),
                StatusCode = ctx.Response.StatusCode
            };
            return httpContextCache;
        }
    }
}