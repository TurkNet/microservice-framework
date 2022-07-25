using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Noctools.TnMon.Api.Controllers.Contracts;
using Noctools.TnMon.Api.Controllers.UseCases;
using Noctools.TnMon.Api.Controllers.UseCases.CloseNocInformation;
using Noctools.TnMon.Api.Assemblers;
using Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs;
using Noctools.Application.CleanArch;
using Noctools.Application.Dtos;

namespace Noctools.TnMon.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/noc-information")]
    public class NocInformationController : ControllerBase
    {
        [HttpPost("outages")]
        [ProducesResponseType(typeof(GetOutageLogsContract), 200)]
        [ProducesResponseType(typeof(ErrorResponseContract), 400)]
        [ProducesResponseType(typeof(ErrorResponseContract), 500)]
        public async Task<IActionResult> GetOutages([FromServices] IMediator eventor,
            GetOuategeLogsCommand request,
            CancellationToken cancellationToken)
        {
            return await eventor.SendStream<GetOuategeLogsCommand, GetOuategeLogsCommandResult>(
                request,
                x => x.ToGetOutageNocInformationContract(),
                cancellationToken);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateNocInformationContract), 200)]
        [ProducesResponseType(typeof(ErrorResponseContract), 400)]
        [ProducesResponseType(typeof(ErrorResponseContract), 500)]
        public async Task<IActionResult> CreateNocInformaiton([FromServices] IMediator eventor,
            CreateNocInformationCommand request,
            CancellationToken cancellationToken)
        {
            return await eventor.SendStream<CreateNocInformationCommand, CreateNocInformationCommandResult>(
                request,
                x => x.ToCreateNocInformationContract(),
                cancellationToken);
        }

        [HttpPatch("{id}/close")]
        [ProducesResponseType(typeof(CloseNocInformationContract), 200)]
        [ProducesResponseType(typeof(ErrorResponseContract), 400)]
        [ProducesResponseType(typeof(ErrorResponseContract), 500)]
        public async Task<IActionResult> CloseNocInformation([FromServices] IMediator eventor, Guid id,
            CloseNocInformationCommand request,
            CancellationToken cancellationToken)
        {
            request.Id = id;
            return await eventor.SendStream<CloseNocInformationCommand, CloseNocInformationCommandResult>(
                request,
                x => x.ToCloseNocInformationContract(),
                cancellationToken);
        }

        // [HttpPut("{id}")]
        // public Task<IActionResult> UpdateNocInformation([FromServices] IMediator eventor, Guid id,
        //     CloseNocInformationCommand request,
        //     CancellationToken cancellationToken)
        // {
        //     return null;
        // }
    }
}
