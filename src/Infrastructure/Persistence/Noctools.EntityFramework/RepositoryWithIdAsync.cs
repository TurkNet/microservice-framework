using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noctools.Domain;

namespace Noctools.EntityFramework
{
    public class RepositoryWithIdAsync<TDbContext, TEntity, TId> : IRepositoryWithIdAsync<TEntity, TId>
        where TDbContext : DbContext
        where TEntity : class, IAggregateRootWithId<TId>
    {
        private readonly TDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryWithIdAsync(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }


        public async Task<TEntity> AddAsync(TEntity entity, params string[] parameters)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, params string[] parameters)
        {
            var entry = _dbSet.Remove(entity);
            return await Task.FromResult(entry.Entity);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, params string[] parameters)
        {
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            return await Task.FromResult(entry.Entity);
        }
    }
}