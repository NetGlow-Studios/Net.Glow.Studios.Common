using System.Linq.Expressions;
using Net.Glow.Studios.Core.Enums.Base;

namespace NetGlowStudios.Database.Repositories.Base.Interfaces;

public interface IBaseRepository<T>
{
    /// <summary>
    ///     Create a new record in database asynchronously.
    /// </summary>
    /// <param name="entity">new entity (Entity: <see cref="T" />)</param>
    public Task<Guid?> CreateAsync(T entity);

    /// <summary>
    ///     Create a new record in database.
    /// </summary>
    /// <param name="entity">new entity (Entity: <see cref="T" />)</param>
    public Guid? Create(T entity);

    /// <summary>
    ///     Create new records in database asynchronously.
    /// </summary>
    /// <param name="entities">(Collection: <see cref="T" />)</param>
    public Task<ICollection<Guid>> CreateManyAsync(ICollection<T> entities);

    /// <summary>
    ///     Create new records in database.
    /// </summary>
    /// <param name="entities">(Collection: <see cref="T" />)</param>
    public ICollection<Guid> CreateMany(ICollection<T> entities);

    /// <summary>
    /// Get count of entities in database table asynchronously.
    /// </summary>
    public Task<int> CountAsync();

    /// <summary>
    /// Get count of entities in database table asynchronously.
    /// </summary>
    public Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get count of entities in database table asynchronously.
    /// </summary>
    public Task<int> CountByStatusAsync(StatusEnum statusEnum);

    /// <summary>
    /// Get count of entities in database table.
    /// </summary>
    public int Count();

    /// <summary>
    /// Get count of entities in database table.
    /// </summary>
    public int CountWhere(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get count of entities in database table.
    /// </summary>
    public int CountByStatus(StatusEnum statusEnum);

    /// <summary>
    ///     Get all records from database asynchronously.
    /// </summary>
    /// <param name="includeProperties">Entity Children</param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetAllAsync(params string[] includeProperties);

    /// <summary>
    ///     Get all records from database.
    /// </summary>
    /// <param name="includeProperties">Entity Children</param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetAll(params string[] includeProperties);


    /// <summary>
    ///     Get all records from database where asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns>Records from table.</returns>
    public Task<ICollection<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);


    /// <summary>
    ///     Get all records from database where.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns>Records from table.</returns>
    public ICollection<T> GetAllWhere(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    /// <summary>
    ///     Get all records with status asynchronously.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetWithStatusAsync(StatusEnum status, params string[] includeProperties);

    /// <summary>
    ///     Get all records with status.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetWithStatus(StatusEnum status, params string[] includeProperties);

    /// <summary>
    ///     Get all records with status asynchronously.
    /// </summary>
    /// <param name="n">Amount</param>
    /// <param name="status">Record status</param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetTopNByStatusAsync(int n, StatusEnum status, params string[] includeProperties);

    /// <summary>
    ///     Get all records with status asynchronously.
    /// </summary>
    /// <param name="n">Amount</param>
    /// <param name="status">Record status</param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetTopNByStatus(int n, StatusEnum status, params string[] includeProperties);

    /// <summary>
    ///     Get all records with ids asynchronously.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public Task<ICollection<T>> GetByIdsAsync(IEnumerable<Guid> ids, params string[] includeProperties);

    /// <summary>
    ///     Get all records with ids asynchronously.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetByIds(IEnumerable<Guid> ids, params string[] includeProperties);

    /// <summary>
    ///     Get first record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    /// <summary>
    ///     Get first record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T GetFirst(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    /// <summary>
    ///     Get first or default record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    /// <summary>
    ///     Get first or default record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    /// <summary>
    ///     Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    /// <summary>
    ///     Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T GetLast(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    ///     Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    /// <summary>
    ///     Get last record with predicate asynchronously.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T? GetLastOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetLastCreatedAsync(params string[] includeProperties);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T? GetLastCreated(params string[] includeProperties);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetLastUpdatedAsync(params string[] includeProperties);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T? GetLastUpdated(params string[] includeProperties);

    /// <summary>
    ///     Get random record asynchronously.
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public Task<T?> GetRandomAsync(params string[] includeProperties);

    /// <summary>
    ///     Get random record.
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T? GetRandom(params string[] includeProperties);


    /// <summary>
    ///     Get record by id asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="includeProperties"></param>
    /// <returns>Record by id.</returns>
    public Task<T?> GetByIdAsync(Guid id, params string[] includeProperties);

    /// <summary>
    ///     Get record by id.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="includeProperties"></param>
    /// <returns>Record by id.</returns>
    public T? GetById(Guid id, params string[] includeProperties);
    

    /// <summary>
    ///     Update record in database asynchronously.
    /// </summary>
    /// <param name="entity">Record to update.</param>
    public Task UpdateAsync(T entity);

    /// <summary>
    ///     Update record in database.
    /// </summary>
    /// <param name="entity">Record to update.</param>
    public void Update(T entity);

    /// <summary>
    ///     Update records in database asynchronously.
    /// </summary>
    /// <param name="entities">Records to update.</param>
    public Task UpdateManyAsync(ICollection<T> entities);

    /// <summary>
    ///     Update records in database asynchronously.
    /// </summary>
    /// <param name="entities">Records to update.</param>
    public void UpdateMany(ICollection<T> entities);

    /// <summary>
    ///     Remove by change status to Delete asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    public Task RemoveSoftAsync(Guid id);

    /// <summary>
    ///     Remove by change status to Delete.
    /// </summary>
    /// <param name="id">Record id.</param>
    public void RemoveSoft(Guid id);

    /// <summary>
    /// Remove all records from database asynchronously.
    /// </summary>
    public Task RemoveSoftAllAsync();

    /// <summary>
    /// Remove all records from database asynchronously where.
    /// </summary>
    /// <param name="predicate"></param>
    public Task RemoveSoftWhereAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Remove all records from database.
    /// </summary>
    public void RemoveSoftAll();

    /// <summary>
    /// Remove all records from database where.
    /// </summary>
    /// <param name="predicate"></param>
    public void RemoveSoftWhere(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Remove record from database asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    public Task RemoveHardAsync(Guid id);

    /// <summary>
    ///     Remove record from database.
    /// </summary>
    /// <param name="id">Record id.</param>
    public void RemoveHard(Guid id);

    /// <summary>
    /// Remove all records from database asynchronously.
    /// </summary>
    public Task RemoveHardAllAsync();

    /// <summary>
    /// Remove all records from database asynchronously where.
    /// </summary>
    /// <param name="predicate"></param>
    public Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Remove all records from database.
    /// </summary>
    public void RemoveHardAll();

    /// <summary>
    /// Remove all records from database.
    /// </summary>
    /// <param name="predicate"></param>
    public void RemoveHardWhere(Expression<Func<T, bool>> predicate);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    
    public bool Any(Expression<Func<T, bool>> predicate);
    
    public Task<bool> AllAsync(Expression<Func<T, bool>> predicate);
    
    public bool All(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    ///     Is record existed by id asynchronously?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <returns>Record exist.</returns>
    public Task<bool> IsWithIdAsync(Guid id);

    /// <summary>
    ///     Is record existed by id?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <returns>Record exist.</returns>
    public bool IsWithId(Guid id);

    /// <summary>
    ///     Is record status Active asynchronously?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <returns>Active status.</returns>
    public Task<bool> IsActiveAsync(Guid id);

    /// <summary>
    ///     Is record status Active?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <returns>Active status.</returns>
    public bool IsActive(Guid id);

    /// <summary>
    ///     Is any record with Active status?
    /// </summary>
    /// <param name="status"></param>
    /// <returns>Active status.</returns>
    public Task<bool> IsWithStatusAsync(StatusEnum status);

    /// <summary>
    ///     Is any record with Active status?
    /// </summary>
    /// <param name="status"></param>
    /// <returns>Active status.</returns>
    public bool IsWithStatus(StatusEnum status);

    /// <summary>
    ///     Execute Raw SQL asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters);

    /// <summary>
    ///     Execute Raw SQL.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public int ExecuteSqlRaw(string sql, params object[] parameters);
}