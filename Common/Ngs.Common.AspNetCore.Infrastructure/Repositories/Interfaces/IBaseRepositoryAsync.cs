using System.Linq.Expressions;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

public interface IBaseRepositoryAsync<T, TId> : IBaseRepositoryReadOnlyAsync<T, TId> 
    where T : class  
    where TId : struct, IEquatable<TId>
{
    /// <summary>
    ///     Create a new record in database asynchronously.
    /// </summary>
    /// <param name="entity">new entity (Entity: <see cref="T" />)</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    public Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Create new records in database asynchronously.
    /// </summary>
    /// <param name="entities">(Collection: <see cref="T" />)</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    public Task<ICollection<T>> CreateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Update record in database asynchronously.
    /// </summary>
    /// <param name="entity">Record to update.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns>Updated record.</returns>
    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Update records in database asynchronously.
    /// </summary>
    /// <param name="entities">Records to update.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>Updated records.</returns>
    public Task<ICollection<T>> UpdateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Remove by change status to Delete asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    public Task RemoveSoftAsync(TId id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    public Task RemoveSoftAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously where.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    public Task RemoveSoftWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Remove record from database asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    public Task RemoveHardAsync(TId id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously where.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    public Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Update additional info asynchronously.
    /// </summary>
    /// <param name="entity"> Entity to update.</param>
    /// <param name="info"></param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns></returns>
    public Task<T> UpdateAdditionalInfoAsync(T entity, string info, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update additional info asynchronously.
    /// </summary>
    /// <param name="id"> Id of entity to update.</param>
    /// <param name="info"> Info to update.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Updated entity.</returns>
    public Task<T> UpdateAdditionalInfoAsync(TId id, string info, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Add tag to entity asynchronously.
    /// </summary>
    /// <param name="entity"> Entity to add tag.</param>
    /// <param name="tag"> Tag to add.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Updated entity.</returns>
    public Task<T> AddTagAsync(T entity, string tag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add tag to entity asynchronously.
    /// </summary>
    /// <param name="id"> Id of entity to add tag.</param>
    /// <param name="tag"> Tag to add.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Updated entity.</returns>
    public Task<T> AddTagAsync(TId id, string tag, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove tag from entity asynchronously.
    /// </summary>
    /// <param name="entity"> Entity to remove tag.</param>
    /// <param name="tag"> Tag to remove.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Updated entity.</returns>
    public Task<T> RemoveTagAsync(T entity, string tag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Remove tag from entity asynchronously.
    /// </summary>
    /// <param name="id"> Id of entity to remove tag.</param>
    /// <param name="tag"> Tag to remove.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> Updated entity.</returns>
    public Task<T> RemoveTagAsync(TId id, string tag, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Check if entity has tag asynchronously.
    /// </summary>
    /// <param name="entity"> Entity to check.</param>
    /// <param name="tag"> Tag to check.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> True if entity has tag, otherwise false.</returns>
    public Task<bool> HasTagAsync(T entity, string tag, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if entity has tag asynchronously.
    /// </summary>
    /// <param name="id"> Id of entity to check.</param>
    /// <param name="tag"> Tag to check.</param>
    /// <param name="cancellationToken"> The token to monitor for cancellation requests.</param>
    /// <returns> True if entity has tag, otherwise false.</returns>
    public Task<bool> HasTagAsync(TId id, string tag, CancellationToken cancellationToken = default);
}