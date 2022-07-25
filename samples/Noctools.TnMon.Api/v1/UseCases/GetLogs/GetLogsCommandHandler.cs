using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Noctools.Application.CleanArch;
using Noctools.Domain;
using Noctools.Domain.Contracts;
using Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs;
using Noctools.TnMon.Api.Domain;
using Noctools.TnMon.Api.Domain.Constants;
using Noctools.TnMon.Api.Assemblers;

namespace Noctools.TnMon.Api.Controllers.UseCases.GetLogs
{
    /// <summary>
    /// Txhandler ile repository'i kullanması application katmanının domain üzerinde bir orchesrator gorevi gordugundendir.
    /// Base olarak bu şekilde geliyor ama repository'i kendimiz de custom olarak yonetebiliriz. Uow olarak kullanmak
    /// her bir microservice'in bir domain objesini transactional olarak kullanmasını kolaylaştırmak içindir.
    /// </summary>
    public class GetLogsCommandHandler : TxRequestHandlerBase<GetLogsCommand, GetLogsCommandResult>
    {
        private readonly ILogQueryRepository _logQueryRepository;

        public GetLogsCommandHandler(IUnitOfWorkAsync uow,
            IQueryRepositoryFactory queryRepositoryFactory,
            ILogQueryRepository logQueryRepository) : base(uow,
            queryRepositoryFactory)
        {
            _logQueryRepository = logQueryRepository;
        }

        public override async Task<GetLogsCommandResult> Handle(GetLogsCommand request,
            CancellationToken cancellationToken)
        {
            var result = await _logQueryRepository.GetLogsAsync(cancellationToken);
            if (!result.Any())
            {
                return new GetLogsCommandResult()
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
                    ReturnPath = "/logs"
                };
            }

            return new GetLogsCommandResult()
            {
                ValidateState = ValidationState.Valid,
                Result = result.Select(x => x.ToDto()),
                ReturnPath = $"/logs"
            };
        }
    }
}
