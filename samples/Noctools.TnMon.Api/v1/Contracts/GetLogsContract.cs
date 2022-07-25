using System.Collections.Generic;
using Noctools.Application.Dtos;
using Noctools.TnMon.Api.Controllers.UseCases;

namespace Noctools.TnMon.Api.Controllers.Contracts
{
    public class GetLogsContract : ApiResponseBaseContract<IEnumerable<GetLogsDto>>
    {
    }
}
