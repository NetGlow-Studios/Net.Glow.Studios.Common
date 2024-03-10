using System.Linq.Expressions;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

public interface IBaseRepositoryAsync<T> : IBaseRepositoryReadOnlyAsync<T> where T : class
{
    /// <summary>
    ///     Create a new record in database asynchronously.
    /// </summary>
    /// <param name="entity">new entity (Entity: <see cref="T" />)</param>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task<T?> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Create new records in database asynchronously.
    /// </summary>
    /// <param name="entities">(Collection: <see cref="T" />)</param>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task<ICollection<T>> CreateManyAsync(ICollection<T> entities,
        CancellationToken cancellationToken = default);

    /// <summary>
    ///     Update record in database asynchronously.
    /// </summary>
    /// <param name="entity">Record to update.</param>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Update records in database asynchronously.
    /// </summary>
    /// <param name="entities">Records to update.</param>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task<ICollection<T>> UpdateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Remove by change status to Delete asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task RemoveSoftAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously.
    /// </summary>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task RemoveSoftAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously where.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task RemoveSoftWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Remove record from database asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task RemoveHardAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously where.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"> (CancellationToken: <see cref="CancellationToken" />)</param>
    public Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}