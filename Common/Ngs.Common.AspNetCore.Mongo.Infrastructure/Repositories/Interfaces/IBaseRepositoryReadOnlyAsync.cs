using System.Linq.Expressions;
using Ngs.Common.AspNetCore.Enums;

namespace Ngs.Common.AspNetCore.Mongo.Infrastructure.Repositories.Interfaces;

public interface IBaseRepositoryReadOnlyAsync<T> where T : class
{
    /// <summary>
    /// Get count of entities in database table asynchronously.
    /// </summary>
    public Task<int> CountAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get count where of entities in database table asynchronously.
    /// </summary>
    public Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get count of entities by status in database table asynchronously.
    /// </summary>
    public Task<int> CountByStatusAsync(StatusEnum statusEnum, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get all records from database asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get all records where from database where asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Records from table.</returns>
    public Task<ICollection<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get all records with status asynchronously.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <param name="cancellationToken"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get all records with status asynchronously.
    /// </summary>
    /// <param name="n">Total</param>
    /// <param name="status">Record status</param>
    /// <param name="cancellationToken"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetTopNByStatusAsync(int n, StatusEnum status, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Get all records with ids asynchronously.
    /// </summary>
    /// <param name="ids"> Records id.</param>
    /// <param name="cancellationToken"> Cancellation token.</param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Get first record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"> Predicate.</param>
    /// <param name="cancellationToken"> Cancellation token.</param>
    /// <returns></returns>
    public Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Get first or default record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> First or default record.</returns>
    public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> Last record.</returns>
    public Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> Last or default record.</returns>
    public Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///   Get last created record asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns> Last created record.</returns>
    public Task<T?> GetLastCreatedAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///  Get last updated record asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns> Last updated record.</returns>
    public Task<T?> GetLastUpdatedAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Get random record asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns> Random record.</returns>
    public Task<T?> GetRandomAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Get record by id asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Record by id.</returns>
    public Task<T?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default);
    
    /// <summary>
    ///    Get page of records asynchronously.
    /// </summary>
    /// <param name="page"> Page number.</param>
    /// <param name="pageSize"> Page size.</param>
    /// <param name="cancellationToken"> Cancellation token.</param>
    /// <returns></returns>
    public Task<ICollection<T>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///  Check if any record exist with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> True if any record exist.</returns>
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///  Check if all records exist with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns> True if all records exist.</returns>
    public Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Is record existed by id asynchronously?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Record exist.</returns>
    public Task<bool> IsWithIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Is record status Active asynchronously?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Active status.</returns>
    public Task<bool> IsActiveAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Is any record with Active status?
    /// </summary>
    /// <param name="status"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Active status.</returns>
    public Task<bool> IsWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default);
}