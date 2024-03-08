using System.Linq.Expressions;
using Ngs.Common.AspNetCore.Enums;

namespace Ngs.Common.AspNetCore.Mongo.Infrastructure.Repositories.Interfaces;

public interface IBaseRepositoryReadOnly<T> where T : class
{
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
    /// <returns>Records from table.</returns>
    public ICollection<T> GetAllWhere(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Get all records with status.
    /// </summary>
    /// <param name="status">Record status</param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetWithStatus(StatusEnum status);

    /// <summary>
    ///     Get all records with status asynchronously.
    /// </summary>
    /// <param name="n">Amount</param>
    /// <param name="status">Record status</param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetTopNByStatus(int n, StatusEnum status);

    /// <summary>
    ///     Get all records with ids asynchronously.
    /// </summary>
    /// <param name="ids"></param>
    /// <returns>All records from table.</returns>
    public ICollection<T> GetByIds(IEnumerable<Guid> ids);

    /// <summary>
    ///     Get first record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public T GetFirst(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Get first or default record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Get last record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public T GetLast(Expression<Func<T, bool>> predicate);

    /// <summary>
    ///     Get last record with predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public T? GetLastOrDefault(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public T? GetLastCreated();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public T? GetLastUpdated();

    /// <summary>
    ///     Get random record.
    /// </summary>
    /// <returns></returns>
    public T? GetRandom();

    /// <summary>
    ///     Get record by id.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <returns>Record by id.</returns>
    public T? GetById(Guid id);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public bool Any(Expression<Func<T, bool>> predicate);
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
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
}