using System.Threading.Tasks;

namespace Noctools.Domain
{
    public interface IRepositoryWithIdAsync<TEntity, TId> where TEntity : IAggregateRootWithId<TId>
    {
        Task<TEntity> AddAsync(TEntity entity, params string[] parameters);
        Task<TEntity> UpdateAsync(TEntity entity, params string[] parameters);
        Task<TEntity> DeleteAsync(TEntity entity, params string[] parameters);
    }
}
