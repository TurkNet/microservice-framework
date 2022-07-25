using System;
using MediatR;
using Newtonsoft.Json;

namespace Noctools.TnMon.Api.Controllers.UseCases.CloseNocInformation
{
    public class CloseNocInformationCommand : IRequest<CloseNocInformationCommandResult>
    {
        [JsonIgnore] public Guid Id { get; set; }
        public string ClosingDescription { get; set; }
    }
}
