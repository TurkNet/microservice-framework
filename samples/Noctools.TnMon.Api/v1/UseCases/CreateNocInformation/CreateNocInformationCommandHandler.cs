using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Noctools.Application.CleanArch;
using Noctools.Domain;
using Noctools.Domain.Contracts;
using Noctools.Infrastructure.Validation;
using Noctools.TnMon.Api.Controllers.UseCases.Dtos;
using Noctools.TnMon.Api.Domain;
using Noctools.TnMon.Api.Domain.Constants;
using Noctools.TnMon.Api.Assemblers;
using Noctools.TnMon.Api.Contants;
using Noctools.TnMon.Api.Infrastructure.Adapters;

namespace Noctools.TnMon.Api.Controllers.UseCases
{
    public class
        CreateNocInformationCommandHandler : TxRequestHandlerBase<CreateNocInformationCommand,
            CreateNocInformationCommandResult>
    {
        private readonly IMailAdapter _mailAdapter;
        private readonly ITicketProxy _ticketProxy;
        private readonly IProductProxy _productProxy;
        private readonly IUnitOfWorkAsync _uow;
        private readonly ILogger _logger;
        private readonly IValidationService _validationService;

        /// <summary>
        /// todo: use typed service factory
        /// </summary>
        /// <param name="mailAdapter"></param>
        /// <param name="ticketProxy"></param>
        /// <param name="productProxy"></param>
        /// <param name="uow"></param>
        /// <param name="queryRepositoryFactory"></param>
        public CreateNocInformationCommandHandler(
            IMailAdapter mailAdapter,
            ITicketProxy ticketProxy,
            IProductProxy productProxy,
            IUnitOfWorkAsync uow,
            IQueryRepositoryFactory queryRepositoryFactory,
            ILogger<CreateNocInformationCommandHandler> logger,
            IValidationService validationService)
            : base(uow, queryRepositoryFactory)
        {
            _uow = uow;
            _mailAdapter = mailAdapter;
            _ticketProxy = ticketProxy;
            _productProxy = productProxy;
            _logger = logger;
            _validationService = validationService;
        }


        public override async Task<CreateNocInformationCommandResult> Handle(CreateNocInformationCommand request,
            CancellationToken cancellationToken)
        {
            var validate = _validationService.Validate(request);
            if (!validate.IsValid)
            {
                return new CreateNocInformationCommandResult()
                {
                    ValidateState = ValidationState.NotAcceptable,
                    Messages = validate.Errors.Select(x => new MessageContractDto()
                    {
                        Code = x.ErrorCode,
                        Title = x.ErrorMessage,
                        Type = MessageType.Error
                    })
                };
            }


            var nocInformation = await NocInformation
                .LoadAsync(request, _ticketProxy, _productProxy, cancellationToken);

            if (request.RecoveryTime.HasValue)
                nocInformation.SetRecoveryTime(request.RecoveryTime);

            if (request.Devices != null)
            {
                foreach (var device in request.Devices)
                    nocInformation.AddDevice(device);
            }

            var nocInformationRepository = _uow.RepositoryAsync<NocInformation>();
            nocInformation = await nocInformationRepository.AddAsync(nocInformation, new string[]
            {
                ApplicationConstants.NocIndex,
                ApplicationConstants.NocIndexType
            });

            /*
             *todo : publish to  message bus next one !
             * _mailAdapter.SendAsync("", "", "", "", cancellationToken);
            */
            return new CreateNocInformationCommandResult() {Result = nocInformation.ToCreateNocInformationDto()};
        }
    }
}