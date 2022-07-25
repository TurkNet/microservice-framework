using System;

namespace Noctools.Domain
{
    public interface IRepositoryAsync<TEntity> : IRepositoryWithIdAsync<TEntity, Guid> where TEntity : IAggregateRoot
    {
    }
}
