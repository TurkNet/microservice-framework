using System.Linq;
using Noctools.TnMon.Api.Controllers.Contracts;
using Noctools.TnMon.Api.Controllers.UseCases;
using Noctools.TnMon.Api.Controllers.UseCases.CloseNocInformation;
using Noctools.TnMon.Api.Controllers.UseCases.CloseNocInformation.Dtos;
using Noctools.TnMon.Api.Controllers.UseCases.Dtos;
using Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Assemblers
{
    public static class NocInformationExtensions
    {
        public static CreateNocInformationDto ToCreateNocInformationDto(this NocInformation nocInformation)
        {
            return new CreateNocInformationDto() {Id = nocInformation.Id};
        }

        public static CloseNocInformationDto ToCloseNocInformationDto(this NocInformation nocInformation)
        {
            return new CloseNocInformationDto() {Id = nocInformation.Id};
        }

        public static GetOutageLogDto ToGetOutageNocInformationDto(this NocInformation nocInformation)
        {
            return new GetOutageLogDto()
            {
                Id = nocInformation.Id,
                TicketId = nocInformation.TicketId,
                Location = nocInformation.PopName,
                HostName = nocInformation.HostName,
                Description = nocInformation.Description,
                LogDate = nocInformation.LogDate,
                DiscoveryDate = nocInformation.DiscoveryDate,
                ClosingDate = nocInformation.ClosingDate,
                Category = nocInformation.Category,
                Status = nocInformation.Status,
                Message = nocInformation.Message,
                RecoveryTime = nocInformation.RecoveryTime,
                Devices = nocInformation.Devices
            };
        }

        public static CreateNocInformationContract ToCreateNocInformationContract(
            this CreateNocInformationCommandResult commandResult)
        {
            return new CreateNocInformationContract()
            {
                Messages = commandResult.Messages?.ToList(),
                Result = commandResult?.Result,
                ReturnPath = commandResult.ReturnPath
            };
        }

        public static CloseNocInformationContract ToCloseNocInformationContract(
            this CloseNocInformationCommandResult commandResult)
        {
            return new CloseNocInformationContract()
            {
                Messages = commandResult.Messages?.ToList(),
                Result = commandResult?.Result,
                ReturnPath = commandResult.ReturnPath
            };
        }

        public static GetOutageLogsContract ToGetOutageNocInformationContract(
            this GetOuategeLogsCommandResult commandResult)
        {
            return new GetOutageLogsContract()
            {
                Messages = commandResult.Messages?.ToList(),
                Result = commandResult?.Result,
                ReturnPath = commandResult.ReturnPath
            };
        }
    }
}
