using System.Linq.Expressions;
using Net.Glow.Studios.Core.Enums.Base;

namespace Net.Glow.Studios.Database.Repositories.Base.Interfaces;

public interface IBaseRepository<T>
{
    /// <summary>
    ///     Create a new record in database.
    /// </summary>
    /// <param name="entity">new entity (Entity: <see cref="T" />)</param>
    public Guid? Create(T entity);

    /// <summary>
    ///     Create new records in database.
    /// </summary>
    /// <param name="entities">(Collection: <see cref="T" />)</param>
    public ICollection<Guid> CreateMany(ICollection<T> entities);

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
    ///     Get all records from database.
    /// </summary>
    /// <param name="includeProperties">Entity Children</param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetAll(params string[] includeProperties);

    /// <summary>
    ///     Get all records from database where.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns>Records from table.</returns>
    public ICollection<T> GetAllWhere(Expression<Func<T, bool>> predicate, params string[] includeProperties);

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
    public ICollection<T> GetTopNByStatus(int n, StatusEnum status, params string[] includeProperties);

    /// <summary>
    ///     Get all records with ids asynchronously.
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="includeProperties"></param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetByIds(IEnumerable<Guid> ids, params string[] includeProperties);

    

    /// <summary>
    ///     Get first record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T GetFirst(Expression<Func<T, bool>> predicate, params string[] includeProperties);

    

    /// <summary>
    ///     Get first or default record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    

    /// <summary>
    ///     Get last record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T GetLast(Expression<Func<T, bool>> predicate, params string[] includeProperties);
    

    /// <summary>
    ///     Get last record with predicate.
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
    public T? GetLastCreated(params string[] includeProperties);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T? GetLastUpdated(params string[] includeProperties);

    /// <summary>
    ///     Get random record.
    /// </summary>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    public T? GetRandom(params string[] includeProperties);

    /// <summary>
    ///     Get record by id.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="includeProperties"></param>
    /// <returns>Record by id.</returns>
    public T? GetById(Guid id, params string[] includeProperties);

    /// <summary>
    ///     Update record in database.
    /// </summary>
    /// <param name="entity">Record to update.</param>
    public void Update(T entity);

    /// <summary>
    ///     Update records in database.
    /// </summary>
    /// <param name="entities">Records to update.</param>
    public void UpdateMany(ICollection<T> entities);

    /// <summary>
    ///     Remove by change status to Delete.
    /// </summary>
    /// <param name="id">Record id.</param>
    public void RemoveSoft(Guid id);

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
    ///     Remove record from database.
    /// </summary>
    /// <param name="id">Record id.</param>
    public void RemoveHard(Guid id);

    /// <summary>
    /// Remove all records from database.
    /// </summary>
    public void RemoveHardAll();

    /// <summary>
    /// Remove all records from database.
    /// </summary>
    /// <param name="predicate"></param>
    public void RemoveHardWhere(Expression<Func<T, bool>> predicate);

   

    public bool Any(Expression<Func<T, bool>> predicate);
    

    public bool All(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Is record existed by id?
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <returns>Record exist.</returns>
    public bool IsWithId(Guid id);

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
    public bool IsWithStatus(StatusEnum status);

    /// <summary>
    ///     Execute Raw SQL.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public int ExecuteSqlRaw(string sql, params object[] parameters);
}