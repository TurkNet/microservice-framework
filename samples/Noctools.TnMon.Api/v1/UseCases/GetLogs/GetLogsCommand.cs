using MediatR;
using Noctools.TnMon.Api.Infrastructure;

namespace Noctools.TnMon.Api.Controllers.UseCases.GetLogs
{
    public class GetLogsCommand : IRequest<GetLogsCommandResult>
    {
    }
}
