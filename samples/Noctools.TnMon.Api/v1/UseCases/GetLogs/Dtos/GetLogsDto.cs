using System;
using Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs;

namespace Noctools.TnMon.Api.Controllers.UseCases
{
    public class GetLogsDto
    {
        public string Id { get; set; }
        public string Index { get; set; }
        public string SyslogHostName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Speed { get; set; }
        public string Threshold { get; set; }
        public int AlertFilter { get; set; }
        public AlertType AlertType { get; set; }
        public string AlertRule { get; set; }
        public string Message { get; set; }
    }
}
