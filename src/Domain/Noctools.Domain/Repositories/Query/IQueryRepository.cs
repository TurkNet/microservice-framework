using System;

namespace Noctools.Domain
{
    public interface IQueryRepository<TEntity> : IQueryRepositoryWithId<TEntity, Guid> where TEntity : IAggregateRoot
    {
    }
}
