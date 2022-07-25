using System.Collections.Generic;
using Noctools.Domain.Commands;

namespace Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs
{
    public class GetOuategeLogsCommandResult : CommandResultBase
    {
        public IEnumerable<GetOutageLogDto> Result { get; set; }
    }
}