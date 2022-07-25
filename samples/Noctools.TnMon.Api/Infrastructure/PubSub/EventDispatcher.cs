using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Models;
using MediatR;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Noctools.Bus;
using Noctools.Domain;
using Noctools.TnMon.Api.Domain;
using Noctools.TnMon.Api.Domain.Constants;
using Noctools.TnMon.Api.Infrastructure;

namespace Noctools.TnMon.Api.Controllers.PubSub
{
    /// <summary>
    /// use for modular monolith and multi aggregate in one bounded context
    /// https://devblogs.microsoft.com/cesardelatorre/using-domain-events-within-a-net-core-microservice/
    /// </summary>
    public class EventDispatcher : INotificationHandler<NotificationEnvelope>
    {
        private ILogger<EventDispatcher> _logger;
        private IMediator _mediator;

        public EventDispatcher(ILogger<EventDispatcher> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        public async Task Handle(NotificationEnvelope notify, CancellationToken cancellationToken)
        {
            switch (notify.Event)
            {
                case NocInformationOpened nocInformationOpened:
                    await _mediator.Publish(nocInformationOpened);
                    break;
                default:
                    throw new DomainException(nameof(DomainErrorCodes.EDAnakin1004), DomainErrorCodes.EDAnakin1004);
            }
        }
    }
}