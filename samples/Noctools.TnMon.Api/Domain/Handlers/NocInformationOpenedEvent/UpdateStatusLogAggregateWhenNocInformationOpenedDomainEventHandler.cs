using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Nest;
using Noctools.Domain;
using Noctools.TnMon.Api.Domain;
using Noctools.TnMon.Api.Domain.Constants;

namespace Noctools.TnMon.Api.Domain
{
    public class
        UpdateStatusLogAggregateWhenNocInformationOpenedDomainEventHandler : INotificationHandler<NocInformationOpened>
    {
        private readonly ILogQueryRepository _logQueryRepository;
        private readonly IUnitOfWorkAsync _uow;

        public UpdateStatusLogAggregateWhenNocInformationOpenedDomainEventHandler(
            ILogQueryRepository logQueryRepository,
            IUnitOfWorkAsync uow)
        {
            _uow = uow;
            _logQueryRepository = logQueryRepository;
        }

        public async Task Handle(NocInformationOpened notification, CancellationToken cancellationToken)
        {
            var commandRepository = _uow.RepositoryAsync<Log>();
            var log = await _logQueryRepository.FindOneAsync(notification.Index, notification.Id, cancellationToken);

            if (log == null)
            {
                throw new DomainException(nameof(DomainErrorCodes.EDAnakin1012),
                    string.Format(DomainErrorCodes.EDAnakin1012, notification.Id));
            }

            log.SetDescription("NocInformationApplied")
                .UpToDate();

            await commandRepository.UpdateAsync(log, new string[] {notification.Index, notification.Id});
        }
    }
}