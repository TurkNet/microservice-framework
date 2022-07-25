using System;
using Microsoft.EntityFrameworkCore;
using Noctools.Domain;

namespace Noctools.EntityFramework
{
    public class RepositoryAsync<TDbContext, TEntity> : RepositoryWithIdAsync<TDbContext, TEntity, Guid>,
        IRepositoryAsync<TEntity>
        where TDbContext : DbContext
        where TEntity : class, IAggregateRoot
    {
        public RepositoryAsync(TDbContext dbContext) : base(dbContext)
        {
        }
    }

    public class RepositoryAsync<TEntity> : RepositoryAsync<DbContext, TEntity>
        where TEntity : class, IAggregateRoot
    {
        public RepositoryAsync(DbContext dbContext) : base(dbContext)
        {
        }
    }
}