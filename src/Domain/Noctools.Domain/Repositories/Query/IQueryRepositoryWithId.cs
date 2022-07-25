using System.Linq;

namespace Noctools.Domain
{
    public interface IQueryRepositoryWithId<TEntity, TId> where TEntity : IAggregateRootWithId<TId>
    {
        IQueryable<TEntity> Queryable(int page = 0, int pageSize = 10);
    }
}
