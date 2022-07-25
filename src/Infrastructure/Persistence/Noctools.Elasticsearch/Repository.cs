using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Nest;
using Noctools.Domain;

namespace Noctools.Elasticsearch
{
    public class Repository<TEntity> : IElasticsearchQueryRepository<TEntity>, IRepositoryAsync<TEntity>
        where TEntity : class, IAggregateRoot
    {
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public Repository(DbContext dbContext, IDomainEventDispatcher domainEventDispatcher)
        {
            DbContext = dbContext;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public DbContext DbContext { get; }

        public virtual IQueryable<TEntity> Queryable(int page = 0, int pageSize = 10)
        {
            var result = DbContext
                .SearchAsync<TEntity>(page, pageSize)
                .Result
                .Documents
                .ToList();

            return result.AsQueryable();
        }

        /// <summary>
        /// parameters.First() is index
        /// parameters.Last() is Type
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity, params string[] parameters)
        {
            var response = await DbContext.IndexAsync<TEntity>(entity,
                parameters.First(),
                parameters.Last(),
                default(CancellationToken));

            if (response.IsValid)
            {
                await PublishDomainEventAsync(entity);
            }

            return await DbContext.FindOneAsync<TEntity>(parameters.First(), response.Id, default(CancellationToken));
        }

        /// <summary>
        /// parameters.First() is index
        /// parameters.Last() is Type
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, params string[] parameters)
        {
            var response = await DbContext.UpdateAsync<TEntity>(entity,
                parameters.First(),
                parameters.Last(),
                default(CancellationToken));
            if (response.IsValid)
            {
                await PublishDomainEventAsync(entity);
            }

            return await DbContext.FindOneAsync<TEntity>(parameters.First(), entity.Id, default(CancellationToken));
        }

        /// <summary>
        /// parameters.First() is index
        /// parameters.Last() is Type
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> DeleteAsync(TEntity entity, params string[] parameters)
        {
            var response = await DbContext.DeleteAsync<TEntity>(entity, default(CancellationToken));
            if (response.IsValid)
            {
                await PublishDomainEventAsync(entity);
            }

            return await DbContext.FindOneAsync<TEntity>(parameters.First(), entity.Id, default(CancellationToken));
        }

        public virtual async Task PublishDomainEventAsync(TEntity entity)
        {
            var events = entity.GetUncommittedEvents().ToArray();
            if (events.Length > 0)
                foreach (var domainEvent in events)
                    await _domainEventDispatcher.Dispatch(domainEvent);
        }
    }
}