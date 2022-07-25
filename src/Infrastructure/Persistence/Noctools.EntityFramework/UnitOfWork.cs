using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Noctools.Domain;

namespace Noctools.EntityFramework
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        private readonly DbContext _context;
        private ConcurrentDictionary<Type, object> _repositories;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public virtual IRepositoryWithIdAsync<TEntity, TId> RepositoryAsync<TEntity, TId>()
            where TEntity : class, IAggregateRootWithId<TId>
        {
            if (_repositories == null) _repositories = new ConcurrentDictionary<Type, object>();

            if (!_repositories.ContainsKey(typeof(TEntity)))
                _repositories[typeof(TEntity)] = new RepositoryWithIdAsync<DbContext, TEntity, TId>(_context);

            return (IRepositoryWithIdAsync<TEntity, TId>)_repositories[typeof(TEntity)];
        }

        public virtual IRepositoryAsync<TEntity> RepositoryAsync<TEntity>()
            where TEntity : class, IAggregateRoot
        {
            if (_repositories == null) _repositories = new ConcurrentDictionary<Type, object>();

            if (!_repositories.ContainsKey(typeof(TEntity)))
                _repositories[typeof(TEntity)] = new RepositoryAsync<TEntity>(_context);

            return (IRepositoryAsync<TEntity>)_repositories[typeof(TEntity)];
        }

        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
