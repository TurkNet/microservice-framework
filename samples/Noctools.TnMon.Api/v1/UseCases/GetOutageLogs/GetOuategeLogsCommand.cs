using System;
using MediatR;

namespace Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs
{
    public class GetOuategeLogsCommand : IRequest<GetOuategeLogsCommandResult>
    {
        public string Status { get; set; }
        public string Category { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
