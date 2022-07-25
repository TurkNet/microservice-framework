using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using Noctools.Domain;
using Noctools.Elasticsearch;
using Noctools.TnMon.Api.Controllers.UseCases.GetOutageLogs;
using Noctools.TnMon.Api.Domain;

namespace Noctools.TnMon.Api.Infrastructure
{
    /// <summary>
    /// /// https://github.com/tungphuong/dotnetcore-microservices-poc/blob/outbox-pattern/PolicySearchService/DataAccess/ElasticSearch/PolicyRepository.cs
    /// </summary>
    public class LogQueryRepository : ILogQueryRepository
    {
        private readonly IElasticsearchQueryRepository<Log> _elasticsearchQueryRepository;

        public LogQueryRepository(IQueryRepositoryFactory queryRepositoryFactory)
        {
            _elasticsearchQueryRepository = queryRepositoryFactory.ElasticsearchQueryRepository<Log>();
        }

        /// <summary>
        /// todo:real search logic
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Log>> GetLogsAsync(CancellationToken cancellationToken)
        {
            var searchQuery = new Nest.SearchDescriptor<Log>()
                .Query(q =>
                    q.MatchAll()
                )
                .From(0)
                .Size(10);

            var response = await _elasticsearchQueryRepository.DbContext.Client
                .SearchAsync<Log>(searchQuery.Index("syslog-*"), cancellationToken);

            var responeWithIds = response.Hits.Select(h =>
            {
                h.Source.SetId(h.Id);
                h.Source.SetIndex(h.Index);
                return h.Source;
            }).ToList();


            return responeWithIds;
        }

        public async Task<Log> FindOneAsync(string index, string id, CancellationToken cancellationToken)
        {
            return await _elasticsearchQueryRepository.DbContext.FindOneAsync<Log>(index, id, cancellationToken);
        }

        private string ToJson<T>(SearchDescriptor<T> searchDescriptor) where T : class
        {
            var stream = new MemoryStream();
            _elasticsearchQueryRepository.DbContext.Client.RequestResponseSerializer
                .Serialize(searchDescriptor, stream);
            var jsonQuery = Encoding.UTF8.GetString(stream.ToArray());
            return jsonQuery;
        }
    }
}
