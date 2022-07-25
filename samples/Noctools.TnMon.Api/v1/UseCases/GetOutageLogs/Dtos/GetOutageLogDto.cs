using System;

namespace Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs
{
    public class GetOutageLogDto
    {
        public Guid Id { get; set; }
        public int TicketId { get; set; }
        public string Location { get; set; }
        public string HostName { get; set; }
        public string Description { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime DiscoveryDate { get; set; }
        public DateTime? RecoveryTime { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string[] Devices { get; set; }
    }
}
