using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using Noctools.Elasticsearch;
using Noctools.Domain;

namespace Noctools.Elasticsearch
{
    /// <summary>
    /// https://github.com/tungphuong/dotnetcore-microservices-poc/blob/outbox-pattern/PolicySearchService/DataAccess/ElasticSearch/PolicyRepository.cs
    /// </summary>
    public class DbContext
    {
        public readonly IElasticClient Client;

        public DbContext(IOptions<ElasticsearchSettings> settings)
        {
            var connectionPool = new StaticConnectionPool(settings.Value.Endpoints.Select(x => new Uri(x.Uri)));
            var connectionSettings = new ConnectionSettings(connectionPool);
            Client = new ElasticClient(connectionSettings);
        }

        public async Task<ISearchResponse<TEntity>> GetAsync<TEntity>(SearchRequest<TEntity> request,
            CancellationToken cancellationToken)
            where TEntity : class, IAggregateRoot
        {
            return await Client.SearchAsync<TEntity>(request, cancellationToken);
        }

        /// <summary>
        /// valid for only noc because log cat not create by this app
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="indexName"></param>
        /// <param name="type"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<IIndexResponse> IndexAsync<TEntity>(TEntity entity, string indexName, string type,
            CancellationToken cancellationToken)
            where TEntity : class, IAggregateRoot
        {
            return await Client.IndexAsync(new IndexRequest<TEntity>(entity, indexName, type),
                cancellationToken);
        }

        /// <summary>
        /// valid for noc and log
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="indexName"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public async Task<IUpdateResponse<TEntity>> UpdateAsync<TEntity>(TEntity entity, string indexName, string id,
            CancellationToken cancellationToken)
            where TEntity : class, IAggregateRoot
        {
            return await Client.UpdateAsync<TEntity>(new DocumentPath<TEntity>(new Id(id)),
                u => u.Index(indexName).Doc(entity));
        }

        public async Task<IDeleteResponse> DeleteAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken,
            Func<DeleteDescriptor<TEntity>, IDeleteRequest> selector = null)
            where TEntity : class, IAggregateRoot
        {
            return await Client.DeleteAsync<TEntity>(entity, selector);
        }

        public async Task<ISearchResponse<TEntity>> SearchAsync<TEntity>(int page, int pageSize)
            where TEntity : class, IAggregateRoot
        {
            var getIndexAttribute =
                (ElasticsearchTypeAttribute) System.Attribute.GetCustomAttribute(typeof(TEntity),
                    typeof(ElasticsearchTypeAttribute));

            return await Client
                .SearchAsync<TEntity>(
                    s =>
                        s.From(page)
                            .Size(pageSize)
                            .Query(q =>
                                q.MatchAll())
                            .Index(getIndexAttribute.Name));
        }


        public async Task<TEntity> FindOneAsync<TEntity>(string index, string id, CancellationToken cancellationToken)
            where TEntity : class, IAggregateRoot
        {
            return await Client.SourceAsync<TEntity>(id, g => g.Index(index), cancellationToken);
        }

        public async Task<TEntity> FindOneAsync<TEntity>(string index, Guid id, CancellationToken cancellationToken)
            where TEntity : class, IAggregateRoot
        {
            return await Client.SourceAsync<TEntity>(id, g => g.Index(index), cancellationToken);
        }
    }
}