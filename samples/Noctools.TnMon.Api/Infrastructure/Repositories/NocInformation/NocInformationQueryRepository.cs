using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Noctools.Domain;
using Noctools.Elasticsearch;
using Noctools.TnMon.Api.Contants;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Infrastructure
{
    /// <summary>
    /// /// https://github.com/tungphuong/dotnetcore-microservices-poc/blob/outbox-pattern/PolicySearchService/DataAccess/ElasticSearch/PolicyRepository.cs
    /// </summary>
    public class NocInformationQueryRepository : INocInformationQueryRepository
    {
        private readonly IElasticsearchQueryRepository<NocInformation> _elasticsearchQueryRepository;

        public NocInformationQueryRepository(IQueryRepositoryFactory queryRepositoryFactory)
        {
            _elasticsearchQueryRepository = queryRepositoryFactory.ElasticsearchQueryRepository<NocInformation>();
        }

        /// <summary>
        /// todo:real search logic
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<NocInformation>> GetOutageLogsAsync(CancellationToken cancellationToken)
        {
            var searchQuery = new Nest.SearchDescriptor<NocInformation>()
                .Query(q =>
                    q.MatchAll()
                )
                .From(0)
                .Size(10);

            var response = await _elasticsearchQueryRepository.DbContext.Client
                .SearchAsync<NocInformation>(searchQuery
                    .Index(ApplicationConstants.NocIndex), cancellationToken);

            return response.Documents;
        }
    }
}