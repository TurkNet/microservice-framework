namespace Noctools.Domain
{
    public interface IRepositoryFactoryAsync
    {
        IRepositoryWithIdAsync<TEntity, TId> RepositoryAsync<TEntity, TId>()
            where TEntity : class, IAggregateRootWithId<TId>;

        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IAggregateRoot;
    }
}
