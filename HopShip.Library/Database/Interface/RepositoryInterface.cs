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

        /// <summary>
        /// Inserisce in blocco (bulk) una collezione di entità in maniera asincrona.
        /// </summary>
        /// <param name="entity">L'entità da inserire</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>L'entità inserita</returns>
        Task<T> InsertAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Inserisce in blocco (bulk) una collezione di entità in maniera asincrona.
        /// </summary>
        /// <param name="entities">Le entità da inserire</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>Le entità inserite</returns>
        Task<IEnumerable<T>> BulkInsertAsync(IEnumerable<T> entities,
        CancellationToken cancellationToken);/// <summary>
                                             /// Elimina un'entità dal database.
                                             /// </summary>
                                             /// <param name="entity">L'entità da eliminare</param>
                                             /// <returns>Un valore booleano che indica se l'eliminazione è avvenuta con successo</returns>
        bool Delete(T entity);

        /// <summary>
        /// Elimina un'entità dal database in modo asincrono.
        /// </summary>
        /// <param name="entity">L'entità da eliminare</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>Un valore booleano che indica se l'eliminazione è avvenuta con successo</returns>
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Elimina in blocco una collezione di entità dal database in modo asincrono.
        /// </summary>
        /// <param name="entities">Le entità da eliminare</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>Un valore booleano che indica se l'eliminazione è avvenuta con successo</returns>
        Task<bool> BulkDeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// Aggiorna un'entità nel database.
        /// </summary>
        /// <param name="entity">L'entità da aggiornare</param>
        /// <returns>L'entità aggiornata</returns>
        T Update(T entity);

        /// <summary>
        /// Aggiorna un'entità nel database in modo asincrono.
        /// </summary>
        /// <param name="entity">L'entità da aggiornare</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>L'entità aggiornata</returns>
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Aggiorna in blocco una collezione di entità nel database in modo asincrono.
        /// </summary>
        /// <param name="entities">Le entità da aggiornare</param>
        /// <param name="cancellationToken">Il token di cancellazione</param>
        /// <returns>Le entità aggiornate</returns>
        Task<IEnumerable<T>> BulkUpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}
