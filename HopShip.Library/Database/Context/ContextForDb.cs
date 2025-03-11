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
        Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken) where TEntity : class;
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

        public async Task BulkInsertAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken) where TEntity : class
        {
            await Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }
    }
}
