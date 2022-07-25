using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Noctools.Application.CleanArch;
using Noctools.Domain;
using Noctools.Elasticsearch;
using Noctools.TnMon.Api.Contants;
using Noctools.TnMon.Api.Controllers.UseCases.CloseNocInformation.Dtos;
using Noctools.TnMon.Api.Domain;
using Noctools.TnMon.Api.Domain.Constants;
using Noctools.TnMon.Api.Assemblers;
using Noctools.TnMon.Api.Infrastructure.Adapters;

namespace Noctools.TnMon.Api.Controllers.UseCases.CloseNocInformation
{
    public class
        CloseNocInformationCommandHandler : TxRequestHandlerBase<CloseNocInformationCommand,
            CloseNocInformationCommandResult>
    {
        private readonly IMailAdapter _mailAdapter;
        private readonly IUnitOfWorkAsync _uow;
        private readonly ILogger _logger;
        private readonly IElasticsearchQueryRepository<NocInformation> _queryRepositoryFactory;

        public CloseNocInformationCommandHandler(
            IMailAdapter mailAdapter,
            ILogger<CloseNocInformationCommandHandler> logger,
            IUnitOfWorkAsync uow,
            IQueryRepositoryFactory queryRepositoryFactory) :
            base(uow, queryRepositoryFactory)
        {
            _mailAdapter = mailAdapter;
            _logger = logger;
            _uow = uow;
            _queryRepositoryFactory = queryRepositoryFactory.ElasticsearchQueryRepository<NocInformation>();
        }

        public override async Task<CloseNocInformationCommandResult> Handle(CloseNocInformationCommand request,
            CancellationToken cancellationToken)
        {
            var commandRepository = _uow.RepositoryAsync<NocInformation>();
            var nocInformation = await _queryRepositoryFactory.DbContext.FindOneAsync<NocInformation>(
                ApplicationConstants.NocIndex, request.Id,
                cancellationToken);

            if (nocInformation == null)
            {
                throw new DomainException(nameof(DomainErrorCodes.EDAnakin1016),
                    string.Format(DomainErrorCodes.EDAnakin1016, nocInformation.Id));
            }

            nocInformation.SetCloseDescription(request.ClosingDescription);


            var result = await commandRepository.UpdateAsync(nocInformation,
                new string[] {ApplicationConstants.NocIndex, nocInformation.Id.ToString()});


            /*
             *todo : publish to  message bus next one !
             * _mailAdapter.SendAsync("", "", "", "", cancellationToken);
            */
            return new CloseNocInformationCommandResult() {Result = result.ToCloseNocInformationDto()};
        }
    }
}