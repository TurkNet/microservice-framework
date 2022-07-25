using System.Linq;
using Noctools.Domain;

namespace Noctools.Elasticsearch
{
    public interface IElasticsearchQueryRepository<TEntity> : IQueryRepository<TEntity>
        where TEntity : IAggregateRoot
    {
        DbContext DbContext { get; }
    }
}
