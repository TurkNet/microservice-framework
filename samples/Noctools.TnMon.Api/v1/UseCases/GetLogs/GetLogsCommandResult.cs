using System.Collections.Generic;
using Noctools.Domain.Commands;

namespace Noctools.TnMon.Api.Controllers.UseCases.GetLogs
{
    public class GetLogsCommandResult : CommandResultBase
    {
        public IEnumerable<GetLogsDto> Result { get; set; }
    }
}
