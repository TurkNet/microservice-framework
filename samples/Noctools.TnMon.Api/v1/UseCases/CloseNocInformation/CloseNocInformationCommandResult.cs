using Noctools.Domain.Commands;
using Noctools.TnMon.Api.Controllers.UseCases.CloseNocInformation.Dtos;

namespace Noctools.TnMon.Api.Controllers.UseCases.CloseNocInformation
{
    public class CloseNocInformationCommandResult : CommandResultBase
    {
        public CloseNocInformationDto Result { get; set; }
    }
}