using System.Linq.Expressions;
using Net.Glow.Studios.Core.Entities.Base;
using Net.Glow.Studios.Core.Enums.Base;

namespace Net.Glow.Studios.Database.Repositories.Base.Interfaces;

public interface IBaseRepositoryReadOnlyAsync<T> where T : BaseEntity
{
    /// <summary>
    /// Get count of entities in database table asynchronously.
    /// </summary>
    public Task<int> CountAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get count where of entities in database table asynchronously.
    /// </summary>
    public Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get count of entities by status in database table asynchronously.
    /// </summary>
    public Task<int> CountByStatusAsync(StatusEnum statusEnum, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Get all records from database asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties">Entity Children</param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default,
        params string[] includeProperties);
    
    /// <summary>
    ///     Get all records where from database where asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns>Records from table.</returns>
    public Task<ICollection<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    ///     Get all records with status asynchronously.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default,
        params string[] includeProperties);
    
    /// <summary>
    ///     Get all records with status asynchronously.
    /// </summary>
    /// <param name="n">Total</param>
    /// <param name="status">Record status</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetTopNByStatusAsync(int n, StatusEnum status,
        CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    ///     Get all records with ids asynchronously.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default,
        params string[] includeProperties);
    
    /// <summary>
    ///     Get first record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default,
        params string[] includeProperties);
    
    /// <summary>
    ///     Get first or default record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    ///     Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default,
        params string[] includeProperties);
    
    /// <summary>
    ///     Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetLastCreatedAsync(CancellationToken cancellationToken = default,
        params string[] includeProperties);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetLastUpdatedAsync(CancellationToken cancellationToken = default,params string[] includeProperties);
    
    /// <summary>
    ///     Get random record asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetRandomAsync(CancellationToken cancellationToken = default,params string[] includeProperties);
    
    /// <summary>
    ///     Get record by id asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"></param>
    /// <param name="includeProperties"></param>
    /// <returns>Record by id.</returns>
    public Task<T?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default, params string[] includeProperties);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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