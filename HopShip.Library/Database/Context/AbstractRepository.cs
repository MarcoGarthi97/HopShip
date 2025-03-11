using HopShip.Library.Database.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Library.Database.Context
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : class
    {
        protected readonly IContextForDb<ContextForDb> Context;

        protected AbstractRepository(IContextForDb<ContextForDb> context)
        {
            Context = context;
        }

        /// <inheritdoc/>
        public int ExecuteSqlRaw(string sql)
        {
            return Context.ExceuteSqlRaw(sql);
        }

        /// <inheritdoc/>
        public Task<int> ExecuteSqlRawAsync(string sql)
        {
            return Context.ExceuteSqlRawAsync(sql);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.FindAsync<T>(predicate);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return Context.FindAsync<T>(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return Context.FirstAsync<T>(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public T? FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return Context.FirstOrDefault<T>(predicate);
        }

        /// <inheritdoc/>
        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return Context.FirstOrDefaultAsync<T>(predicate);
        }

        /// <inheritdoc/>
        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            return Context.FirstOrDefaultAsync<T>(predicate, cancellationToken);
        }

        /// <inheritdoc/>
        public Task BulkInsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            return Context.BulkInsertAsync<T>(entities, cancellationToken);
        }
    }
}
