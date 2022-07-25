using System.Collections.Generic;

namespace Noctools.Application.Logging.Serilog.Enrichers.HttpContext
{
    public class HttpContextCache
    {
        public string IpAddress { get; set; }

        public string Host { get; set; }

        public string Path { get; set; }

        public bool IsHttps { get; set; }

        public string Scheme { get; set; }

        public string Method { get; set; }

        public string ContentType { get; set; }

        public string Protocol { get; set; }

        public string QueryString { get; set; }

        public Dictionary<string, string> Query { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public Dictionary<string, string> Cookies { get; set; }

        public string TraceIdentifier { get; set; }

        public string CorrelationId { get; set; }

        public int StatusCode { get; set; }
    }
}
