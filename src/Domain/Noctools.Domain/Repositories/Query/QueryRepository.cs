namespace Noctools.Domain
{
    public interface IQueryRepositoryFactory
    {
        IQueryRepositoryWithId<TEntity, TId> QueryRepository<TEntity, TId>()
            where TEntity : class, IAggregateRootWithId<TId>;

        IQueryRepository<TEntity> QueryRepository<TEntity>() where TEntity : class, IAggregateRoot;
    }
}
