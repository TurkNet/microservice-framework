using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Noctools.Application.CleanArch;
using Noctools.Domain;
using Noctools.Domain.Contracts;
using Noctools.TnMon.Api.Assemblers;
using Noctools.TnMon.Api.Domain;
using Noctools.TnMon.Api.Domain.Constants;

namespace Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs
{
    public class GetOuategeLogsCommandHandler : TxRequestHandlerBase<GetOuategeLogsCommand, GetOuategeLogsCommandResult>
    {
        private readonly INocInformationQueryRepository _nocInformationQueryRepository;
        private readonly ILogger _logger;

        public GetOuategeLogsCommandHandler(
            IUnitOfWorkAsync uow,
            IQueryRepositoryFactory queryRepositoryFactory,
            INocInformationQueryRepository nocInformationQueryRepository,
            ILogger<GetOuategeLogsCommandHandler> logger) :
            base(uow, queryRepositoryFactory)
        {
            _nocInformationQueryRepository = nocInformationQueryRepository;
            _logger = logger;
        }

        public override async Task<GetOuategeLogsCommandResult> Handle(GetOuategeLogsCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _nocInformationQueryRepository.GetOutageLogsAsync(cancellationToken);
            if (!result.Any())
            {
                return new GetOuategeLogsCommandResult()
                {
                    ValidateState = ValidationState.DoesNotExist,
                    Messages = new[]
                    {
                        new MessageContractDto()
                        {
                            Code = nameof(DomainErrorCodes.EDAnakin1001),
                            Content = DomainErrorCodes.EDAnakin1001,
                            Title = "Error",
                            Type = MessageType.Error
                        }
                    },
                    ReturnPath = "/outages"
                };
            }

            return new GetOuategeLogsCommandResult()
            {
                ValidateState = ValidationState.Valid,
                Result = result.Select(x => x.ToGetOutageNocInformationDto()),
                ReturnPath = $"/outages"
            };
        }
    }
}