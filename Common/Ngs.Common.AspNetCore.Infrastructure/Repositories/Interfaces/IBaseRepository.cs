using System.Linq.Expressions;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

public interface IBaseRepository<T, in TId> : IBaseRepositoryReadOnly<T, TId> 
    where T : class  
    where TId : struct, IEquatable<TId>
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
    public ICollection<T> CreateMany(ICollection<T> entities);

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
    public void RemoveSoft(TId id);

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
    public void RemoveHard(TId id);

    /// <summary>
    /// Remove all records from database.
    /// </summary>
    /// <param name="predicate"></param>
    public void RemoveHardWhere(Expression<Func<T, bool>> predicate);
    
    /// <summary>
    /// Update additional info for entity.
    /// </summary>
    /// <param name="entity"> Entity to update.</param>
    /// <param name="info"> Additional info.</param>
    /// <returns> Updated entity.</returns>
    public T UpdateAdditionalInfo(T entity, string info);

    /// <summary>
    /// Update additional info for entity.
    /// </summary>
    /// <param name="id"> Entity id.</param>
    /// <param name="info"> Additional info.</param>
    /// <returns> Updated entity.</returns>
    public T UpdateAdditionalInfo(TId id, string info);
    
    /// <summary>
    /// Get entity by id.
    /// </summary>
    /// <param name="entity"> Entity to update.</param>
    /// <param name="tag"> Tag to add.</param>
    /// <returns> Updated entity.</returns>
    public T AddTag(T entity, string tag);
    
    /// <summary>
    /// Add tag to entity.
    /// </summary>
    /// <param name="id"> Entity id.</param>
    /// <param name="tag"> Tag to add.</param>
    /// <returns> Updated entity.</returns>
    public T AddTag(TId id, string tag);
    
    /// <summary>
    /// Remove tag from entity.
    /// </summary>
    /// <param name="entity"> Entity to update.</param>
    /// <param name="tag"> Tag to remove.</param>
    /// <returns> Updated entity.</returns>
    public T RemoveTag(T entity, string tag);
    
    /// <summary>
    /// Remove tag from entity.
    /// </summary>
    /// <param name="id"> Entity id.</param>
    /// <param name="tag"> Tag to remove.</param>
    /// <returns> Updated entity.</returns>
    public T RemoveTag(TId id, string tag);
    
    /// <summary>
    /// Check if entity has tag.
    /// </summary>
    /// <param name="entity"> Entity to check.</param>
    /// <param name="tag"> Tag to check.</param>
    /// <returns> True if entity has tag.</returns>
    public bool HasTag(T entity, string tag);
    
    /// <summary>
    /// Check if entity has tag.
    /// </summary>
    /// <param name="id"> Entity id.</param>
    /// <param name="tag"> Tag to check.</param>
    /// <returns> True if entity has tag.</returns>
    public bool HasTag(TId id, string tag);
}