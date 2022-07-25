using System.Collections.Generic;
using Noctools.Application.Dtos;
using Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs;

namespace Noctools.TnMon.Api.Controllers.Contracts
{
    public class GetOutageLogsContract : ApiResponseBaseContract<IEnumerable<GetOutageLogDto>>
    {
    }
}