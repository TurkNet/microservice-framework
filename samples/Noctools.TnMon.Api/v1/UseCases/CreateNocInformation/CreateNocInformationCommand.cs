using System;
using MediatR;


namespace Noctools.TnMon.Api.Controllers.UseCases
{
    public class CreateNocInformationCommand : IRequest<CreateNocInformationCommandResult>
    {
        public string Category { get; set; }
        public string HostName { get; set; }
        public string Message { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime? RecoveryTime { get; set; }
        public DateTime DiscoveryDate { get; set; }
        public string Description { get; set; }
        public string PopName { get; set; }
        public string LogIndexName { get; set; }
        public string LogIndexId { get; set; }
        public string[] Devices { get; set; }
    }
}
