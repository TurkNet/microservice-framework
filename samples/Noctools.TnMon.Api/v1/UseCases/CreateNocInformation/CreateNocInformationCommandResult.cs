using Noctools.Domain.Commands;
using Noctools.TnMon.Api.Controllers.UseCases.Dtos;

namespace Noctools.TnMon.Api.Controllers.UseCases
{
    public class CreateNocInformationCommandResult : CommandResultBase
    {
        public CreateNocInformationDto Result { get; set; }
    }
}