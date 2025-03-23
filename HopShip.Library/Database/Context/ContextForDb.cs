using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Library.Database.Context
{
    public interface IContextForDb<TContext> where TContext : DbContext
    {
        int ExceuteSqlRaw(string sql);
        Task<int> ExceuteSqlRawAsync(string sql);
        Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class;
        TEntity? FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : class;
        Task<IEnumerable<TEntity>> BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities,
        CancellationToken cancellationToken) where TEntity : class; bool Delete<TEntity>(TEntity entity) where TEntity : class;
        Task<bool> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        Task<bool> BulkDeleteAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        Task<IEnumerable<TEntity>> BulkUpdateAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;
    }

    public class ContextForDb : DbContextModel, IContextForDb<ContextForDb>
    {
        public ContextForDb(DbContextOptions<ContextForDb> options)
        : base(options)
        {
        }

        public int ExceuteSqlRaw(string sql)
        {
            return Database.ExecuteSqlRaw(sql);
        }

        public async Task<int> ExceuteSqlRawAsync(string sql)
        {
            return await Database.ExecuteSqlRawAsync(sql);
        }

        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await Set<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {
            return await Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<TEntity> FirstAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {
            return await Set<TEntity>().FirstAsync(predicate);
        }

        public async Task<TEntity?> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return await Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public TEntity? FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return Set<TEntity>().FirstOrDefaultAsync(predicate).Result;
        }

        public async Task<TEntity?> FirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) where TEntity : class
        {
            return await Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        where TEntity : class
        {
            await Set<TEntity>().AddAsync(entity, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> BulkInsertAsync<TEntity>(
        IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        where TEntity : class
        {
            await Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            return entities;
        }
        public bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
            return SaveChanges() > 0;
        }

        public async Task<bool> DeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> BulkDeleteAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Update(entity);
            SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            Set<TEntity>().Update(entity);
            await SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> BulkUpdateAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            Set<TEntity>().UpdateRange(entities);
            await SaveChangesAsync(cancellationToken);
            return entities;
        }
    }
}
