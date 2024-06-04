using System.Linq.Expressions;
using Ngs.Common.AspNetCore.Enums;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

public interface IBaseRepositoryReadOnlyAsync<T, in TId> : IBaseRepo where T : class where TId : struct, IEquatable<TId>
{
    #region Count

    /// <summary>
    /// Get count of entities in database table asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Count of entities. </returns>
    public Task<int> CountAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get count of entities by predicate in database table asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Count of entities. </returns>
    public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get count of entities by status in database table asynchronously.
    /// </summary>
    /// <param name="statusEnum"> Record status.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Count of entities. </returns>
    public Task<int> CountAsync(StatusEnum statusEnum, CancellationToken cancellationToken = default);

    #endregion

    #region GetAll

    /// <summary>
    /// Get all records from database asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties">Entity Children. </param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records where from database where asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate. </param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children. </param>
    /// <returns>Records from table.</returns>
    public Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);

    /// <summary>
    /// Get all records with status asynchronously.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetAllAsync(StatusEnum status, CancellationToken cancellationToken = default, params string[] includeProperties);

    /// <summary>
    /// Get all records with status asynchronously.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetAllAsync(StatusEnum status, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);

    #endregion

    #region GetAllOrdered

    /// <summary>
    /// Get all records ordered by expression asynchronously.
    /// </summary>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public Task<ICollection<T>> GetAllOrderedAsync(Expression<Func<T, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression asynchronously.
    /// </summary>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="ascending"> Ascending order. If true, ascending order, otherwise descending order.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public Task<ICollection<T>> GetAllOrderedAsync(Expression<Func<T, object>> orderBy, bool ascending, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public Task<ICollection<T>> GetAllOrderedAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="ascending"> Ascending order. If true, ascending order, otherwise descending order.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public Task<ICollection<T>> GetAllOrderedAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, bool ascending, CancellationToken cancellationToken = default, params string[] includeProperties);

    /// <summary>
    /// Get all records ordered by expression asynchronously.
    /// </summary>
    /// <param name="status"> Record status.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public Task<ICollection<T>> GetAllOrderedAsync(StatusEnum status, Expression<Func<T, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties);

    /// <summary>
    /// Get all records ordered by expression asynchronously.
    /// </summary>
    /// <param name="status"> Record status.</param>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public Task<ICollection<T>> GetAllOrderedAsync(StatusEnum status, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression asynchronously.
    /// </summary>
    /// <param name="statuses"> Record statuses.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public Task<ICollection<T>> GetAllOrderedAsync(StatusEnum[] statuses, Expression<Func<T, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties);

    #endregion

    #region Statuses
    
    /// <summary>
    /// Get all records with Active status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllActiveAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Active status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllActiveAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Draft status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllDraftAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Draft status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllDraftAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Hidden status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllHiddenAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Hidden status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllHiddenAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Inactive status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllInactiveAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Inactive status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllInactiveAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Outdated status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllOutdatedAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Outdated status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllOutdatedAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Archived status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllArchivedAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Archived status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllArchivedAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Suspended status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllSuspendedAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Suspended status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllSuspendedAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Locked status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllLockedAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Locked status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllLockedAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Pending Activation status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllPendingActivationAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Pending Activation status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllPendingActivationAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Pending Deactivation status asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllPendingDeactivationAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get all records with Pending Deactivation status asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetAllPendingDeactivationAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);

    /// <summary>
    /// Get all records without status asynchronously.
    /// </summary>
    /// <param name="status"> Record status</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetWithoutStatusAsync(StatusEnum status, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    #endregion

    #region GetTopN

    /// <summary>
    /// Get top N records asynchronously.
    /// </summary>
    /// <param name="number">Total number of records.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>All records from table.</returns>
    public  Task<ICollection<T>> GetTopNAsync(int number, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get top N records with status asynchronously.
    /// </summary>
    /// <param name="status"> Record status.</param>
    /// <param name="number"> Total number of records.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetTopNAsync(StatusEnum status, int number, CancellationToken cancellationToken = default, params string[] includeProperties);

    /// <summary>
    /// Get top N records with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="number"> Total number of records.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public Task<ICollection<T>> GetTopNAsync(Expression<Func<T, bool>> predicate, int number, CancellationToken cancellationToken = default, params string[] includeProperties);

    #endregion

    #region GetById

    /// <summary>
    /// Get all records with ids asynchronously.
    /// </summary>
    /// <param name="ids"> Record ids.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetByIdsAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default, params string[] includeProperties);

    /// <summary>
    /// Get record by id asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>Record by id.</returns>
    public Task<T?> GetByIdAsync(TId id,CancellationToken cancellationToken = default, params string[] includeProperties);

    #endregion

    #region GetFirst

    /// <summary>
    /// Get first record asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> First record from table.</returns>
    public Task<T> GetFirstAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get first record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> First record from table.</returns>
    public Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get first or default record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> First record from table.</returns>
    public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);

    #endregion

    #region GetLast

    /// <summary>
    /// Get last record asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Last record from table.</returns>
    public Task<T?> GetLastAsync(CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Last record from table.</returns>
    public Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Last record from table or null.</returns>
    public Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// Get last updated record asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Last updated record from table.</returns>
    public Task<T?> GetLastUpdatedAsync(CancellationToken cancellationToken = default,params string[] includeProperties);

    #endregion

    #region GetRandom

    /// <summary>
    /// Get random record asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Random record from table. </returns>
    public Task<T> GetRandomAsync(CancellationToken cancellationToken = default,params string[] includeProperties);

    #endregion

    #region GetPage

    /// <summary>
    /// Get page of records asynchronously.
    /// </summary>
    /// <param name="page"> Page number.</param>
    /// <param name="pageSize"> Page size.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Page of records.</returns>
    public Task<ICollection<T>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default, params string[] includeProperties);

    /// <summary>
    /// Get page of records with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="page"> Page number.</param>
    /// <param name="pageSize"> Page size.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Page of records.</returns>
    public Task<ICollection<T>> GetPageAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, CancellationToken cancellationToken = default, params string[] includeProperties);

    #endregion

    #region Conditionals

    /// <summary>
    /// Asynchronously determines whether any element of a sequence satisfies a condition.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Any record exist.</returns>
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Asynchronously determines whether all elements of a sequence satisfy a condition.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> All records exist.</returns>
    public Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Is record existed by id asynchronously?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns>Record exist.</returns>
    public Task<bool> IsWithIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Is record status Active asynchronously?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns>Active status.</returns>
    public Task<bool> IsActiveAsync(TId id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Is any record with Active status?
    /// </summary>
    /// <param name="status"> Record status.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns>Active status.</returns>
    public Task<bool> IsWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default);

    /// <summary>
    /// Is record with status asynchronously?
    /// </summary>
    /// <param name="id"> Record id.</param>
    /// <param name="status"> Record status.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Record with status.</returns>
    public Task<bool> HasStatusAsync(TId id, StatusEnum status, CancellationToken cancellationToken = default);

    #endregion
}