using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Noctools.Application.CleanArch;
using Noctools.Application.Dtos;
using Noctools.TnMon.Api.Controllers.Contracts;
using Noctools.TnMon.Api.Controllers.UseCases;
using Noctools.TnMon.Api.Controllers.UseCases.GetLogs;
using Noctools.TnMon.Api.Assemblers;

namespace Noctools.TnMon.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/anakin")]
    public class LogController : ControllerBase
    {
        /// <summary>
        /// todo:use fx paginated item for pagination 
        /// </summary>
        /// <param name="eventor"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("logs")]
        [ProducesResponseType(typeof(GetLogsContract), 200)]
        [ProducesResponseType(typeof(ErrorResponseContract), 400)]
        [ProducesResponseType(typeof(ErrorResponseContract), 500)]
        public async Task<IActionResult> GetLogs([FromServices] IMediator eventor,
            CancellationToken cancellationToken)
        {
            return await eventor.SendStream<GetLogsCommand, GetLogsCommandResult>(
                new GetLogsCommand(),
                x => x.ToContract(),
                cancellationToken);
        }


        [HttpGet("thresholds")]
        public ActionResult<IEnumerable<string>> GetThresholds(int id)
        {
            return new string[] {"value1", "value2"};
        }

        [HttpGet("downs")]
        public ActionResult<IEnumerable<string>> GetDowns(int id)
        {
            return new string[] {"value1", "value2"};
        }

        [HttpGet("histories")]
        public ActionResult<IEnumerable<string>> GetHistories(int id)
        {
            return new string[] {"value1", "value2"};
        }
    }
}