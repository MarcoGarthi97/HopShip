using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HopShip.Library.Database.Interface
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Esegue una query SQL raw in modo sincrono.
        /// </summary>
        /// <param name="sql">La query SQL da eseguire</param>
        /// <returns>Il numero di righe interessate</returns>
        int ExecuteSqlRaw(string sql);

        /// <summary>
        /// Esegue una query SQL raw in modo asincrono.
        /// </summary>
        /// <param name="sql">La query SQL da eseguire</param>
        /// <returns>Il numero di righe interessate</returns>
        Task<int> ExecuteSqlRawAsync(string sql);

        /// <summary>
        /// Trova tutte le entità che soddisfano un predicato in modo asincrono.
        /// </summary>
        /// <param name="predicate">L'espressione di filtro</param>
        /// <returns>Una collezione di entità che soddisfano il predicato</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Trova tutte le entità che soddisfano un predicato in modo asincrono con token di cancellazione.
        /// </summary>
        /// <param name="predicate">L'espressione di filtro</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>Una collezione di entità che soddisfano il predicato</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Ottiene la prima entità che soddisfa un predicato in modo asincrono.
        /// </summary>
        /// <param name="predicate">L'espressione di filtro</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>La prima entità che soddisfa il predicato</returns>
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Ottiene la prima entità che soddisfa un predicato o null se nessuna entità soddisfa il predicato.
        /// </summary>
        /// <param name="predicate">L'espressione di filtro</param>
        /// <returns>La prima entità che soddisfa il predicato o null</returns>
        T? FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Ottiene la prima entità che soddisfa un predicato o null se nessuna entità soddisfa il predicato in modo asincrono.
        /// </summary>
        /// <param name="predicate">L'espressione di filtro</param>
        /// <returns>La prima entità che soddisfa il predicato o null</returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Ottiene la prima entità che soddisfa un predicato o null se nessuna entità soddisfa il predicato in modo asincrono con token di cancellazione.
        /// </summary>
        /// <param name="predicate">L'espressione di filtro</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>La prima entità che soddisfa il predicato o null</returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}
