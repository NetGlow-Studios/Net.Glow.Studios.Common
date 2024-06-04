using System.Linq.Expressions;
using Ngs.Common.AspNetCore.Enums;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

public interface IBaseRepositoryReadOnly<T, in TId> : IBaseRepo where T : class where TId : struct, IEquatable<TId>
{
    /// <summary>
    /// Get count of entities in database table.
    /// </summary>
    /// <returns> Count of entities. </returns>
    public int Count();
    
    /// <summary>
    /// Get count of entities by predicate in database table.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <returns> Count of entities. </returns>
    public int Count(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get count of entities by status in database table.
    /// </summary>
    /// <param name="statusEnum"> Record status.</param>
    /// <returns> Count of entities. </returns>
    public int Count(StatusEnum statusEnum);
    
    /// <summary>
    /// Get all records from database.
    /// </summary>
    /// <param name="includeProperties"> Entity Children. </param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetAll(params string[] includeProperties);
    
    /// <summary>
    /// Get all records where from database where.
    /// </summary>
    /// <param name="predicate"> Predicate. </param>
    /// <param name="includeProperties"> Entity children. </param>
    /// <returns>Records from table.</returns>
    public ICollection<T> GetAll(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    /// <summary>
    /// Get all records with status.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetAll(StatusEnum status, params string[] includeProperties);

    /// <summary>
    /// Get all records with status.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetAll(StatusEnum status, Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression.
    /// </summary>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public ICollection<T> GetAllOrdered(Expression<Func<T, object>> orderBy, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression.
    /// </summary>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="ascending"> Ascending order. If true, ascending order, otherwise descending order.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public ICollection<T> GetAllOrdered(Expression<Func<T, object>> orderBy, bool ascending, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public ICollection<T> GetAllOrdered(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="ascending"> Ascending order. If true, ascending order, otherwise descending order.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public ICollection<T> GetAllOrdered(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, bool ascending, params string[] includeProperties);

    /// <summary>
    /// Get all records ordered by expression.
    /// </summary>
    /// <param name="status"> Record status.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public ICollection<T> GetAllOrdered(StatusEnum status, Expression<Func<T, object>> orderBy, params string[] includeProperties);

    /// <summary>
    /// Get all records ordered by expression.
    /// </summary>
    /// <param name="status"> Record status.</param>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public ICollection<T> GetAllOrdered(StatusEnum status, Expression<Func<T, bool>> predicate, Expression<Func<T, object>> orderBy, params string[] includeProperties);
    
    /// <summary>
    /// Get all records ordered by expression.
    /// </summary>
    /// <param name="statuses"> Record statuses.</param>
    /// <param name="orderBy"> Order by expression.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table. </returns>
    public Task<ICollection<T>> GetAllOrdered(StatusEnum[] statuses, Expression<Func<T, object>> orderBy, params string[] includeProperties);

    #region Statuses

    /// <summary>
    /// Get all active records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetActive(params string[] includeProperties);
    
    /// <summary>
    /// Get all active records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetActive(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all draft records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetDraft(params string[] includeProperties);
    
    /// <summary>
    /// Get all draft records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetDraft(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all hidden records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetHidden(params string[] includeProperties);
    
    /// <summary>
    /// Get all hidden records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetHidden(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all inactive records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetInactive(params string[] includeProperties);
    
    /// <summary>
    /// Get all inactive records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetInactive(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all outdated records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetOutdated(params string[] includeProperties);
    
    /// <summary>
    /// Get all outdated records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetOutdated(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all archived records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetArchived(params string[] includeProperties);
    
    /// <summary>
    /// Get all archived records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetArchived(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all expired records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetSuspended(params string[] includeProperties);
    
    /// <summary>
    /// Get all expired records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetSuspended(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all locked records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetLocked(params string[] includeProperties);
    
    /// <summary>
    /// Get all locked records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetLocked(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all pending activation records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetPendingActivation(params string[] includeProperties);
    
    /// <summary>
    /// Get all pending activation records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetPendingActivation(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get all pending deactivation records.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetPendingDeactivation(params string[] includeProperties);
    
    /// <summary>
    /// Get all pending deactivation records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetPendingDeactivation(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    #endregion
    
    /// <summary>
    /// Get all records without status.
    /// </summary>
    /// <param name="status"> Record status</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetWithoutStatus(StatusEnum status, params string[] includeProperties);

    /// <summary>
    /// Get top N records.
    /// </summary>
    /// <param name="number">Total number of records.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>All records from table.</returns>
    public  ICollection<T> GetTopN(int number, params string[] includeProperties);
    
    /// <summary>
    /// Get top N records with status.
    /// </summary>
    /// <param name="status"> Record status.</param>
    /// <param name="number"> Total number of records.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetTopN(StatusEnum status, int number, params string[] includeProperties);

    /// <summary>
    /// Get top N records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="number"> Total number of records.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Records from table.</returns>
    public ICollection<T> GetTopN(Expression<Func<T, bool>> predicate, int number, params string[] includeProperties);

    /// <summary>
    /// Get all records with ids.
    /// </summary>
    /// <param name="ids"> Record ids.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetByIds(IEnumerable<TId> ids, params string[] includeProperties);

    /// <summary>
    /// Get record by id.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns>Record by id.</returns>
    public T? GetById(TId id, params string[] includeProperties);
    
    /// <summary>
    /// Get first record.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> First record from table.</returns>
    public T GetFirst(params string[] includeProperties);
    
    /// <summary>
    /// Get first record with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> First record from table.</returns>
    public T GetFirst(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get first or default record with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> First record from table.</returns>
    public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get last record.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Last record from table.</returns>
    public T? GetLast(params string[] includeProperties);
    
    /// <summary>
    /// Get last record with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Last record from table.</returns>
    public T GetLast(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get last record with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Last record from table or null.</returns>
    public T? GetLastOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// Get last updated record.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Last updated record from table.</returns>
    public T? GetLastUpdated(params string[] includeProperties);
    
    /// <summary>
    /// Get random record.
    /// </summary>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Random record from table. </returns>
    public T GetRandom(params string[] includeProperties);
    
    /// <summary>
    /// Get page of records.
    /// </summary>
    /// <param name="page"> Page number.</param>
    /// <param name="pageSize"> Page size.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Page of records.</returns>
    public ICollection<T> GetPage(int page, int pageSize, params string[] includeProperties);

    /// <summary>
    /// Get page of records with predicate.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="page"> Page number.</param>
    /// <param name="pageSize"> Page size.</param>
    /// <param name="includeProperties"> Entity children.</param>
    /// <returns> Page of records.</returns>
    public ICollection<T> GetPage(Expression<Func<T, bool>> predicate, int page, int pageSize, params string[] includeProperties);
    
    /// <summary>
    /// Determines whether any element of a sequence satisfies a condition.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <returns> Any record exist.</returns>
    public bool Any(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Determines whether all elements of a sequence satisfy a condition.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <returns> All records exist.</returns>
    public bool All(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Is record existed by id asynchronously?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <returns>Record exist.</returns>
    public bool IsWithId(TId id);

    /// <summary>
    /// Is record status Active?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <returns>Active status.</returns>
    public bool IsActive(TId id);
    
    /// <summary>
    /// Is any record with Active status?
    /// </summary>
    /// <param name="status"> Record status.</param>
    /// <returns>Active status.</returns>
    public bool IsWithStatus(StatusEnum status);

    /// <summary>
    /// Is record with status?
    /// </summary>
    /// <param name="id"> Record id.</param>
    /// <param name="status"> Record status.</param>
    /// <returns> Record with status.</returns>
    public bool HasStatus(TId id, StatusEnum status);
}