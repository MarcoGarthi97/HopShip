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
        public Task<T> InsertAsync(T entity, CancellationToken cancellationToken)
        {
            return Context.InsertAsync<T>(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T>> BulkInsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            return Context.BulkInsertAsync<T>(entities, cancellationToken);
        }/// <inheritdoc/>
        public bool Delete(T entity)
        {
            return Context.Delete(entity);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            return Context.DeleteAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> BulkDeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            return Context.BulkDeleteAsync(entities, cancellationToken);
        }

        /// <inheritdoc/>
        public T Update(T entity)
        {
            return Context.Update(entity);
        }

        /// <inheritdoc/>
        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            return Context.UpdateAsync(entity, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T>> BulkUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            return Context.BulkUpdateAsync(entities, cancellationToken);
        }
    }
}
