using System.Linq;
using Noctools.TnMon.Api.Controllers.Contracts;
using Noctools.TnMon.Api.Controllers.UseCases;
using Noctools.TnMon.Api.Controllers.UseCases.GetLogs;
using Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Assemblers
{
    public static class LogExtensions
    {
        public static GetLogsDto ToDto(this Log log)
        {
            return new GetLogsDto()
            {
                Id = log.Id,
                SyslogHostName = log.HostName,
                Message = log.Message,
                Speed = log.Speed,
                Threshold = log.Threshold,
                Timestamp = log.TimeStamp,
                AlertFilter = log.AlertFilter,
                AlertRule = log.AlertRule,
                AlertType = (AlertType)log.AlertType
            };
        }

        public static GetLogsContract ToContract(this GetLogsCommandResult commandResult)
        {
            return new GetLogsContract()
            {
                Messages = commandResult.Messages?.ToList(),
                Result = commandResult.Result,
                ReturnPath = commandResult.ReturnPath
            };
        }
    }
}
