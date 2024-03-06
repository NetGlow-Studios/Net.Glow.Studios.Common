using System.Linq.Expressions;
using Ngs.Common.AspNetCore.Entities;

namespace Ngs.Common.AspNetCore.Mongo.Infrastructure.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    /// <summary>
    ///     Create a new record in database.
    /// </summary>
    /// <param name="entity">new entity (Entity: <see cref="T" />)</param>
    public T Create(T entity);

    /// <summary>
    ///     Create new records in database.
    /// </summary>
    /// <param name="entities">(Collection: <see cref="T" />)</param>
    public IReadOnlyCollection<T> CreateMany(ICollection<T> entities);

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
    /// <param name="predicate"></param>
    public void RemoveHardWhere(Expression<Func<T, bool>> predicate);
}