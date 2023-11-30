using System.Linq.Expressions;
using Net.Glow.Studios.Core.Entities.Base;

namespace Net.Glow.Studios.Database.Repositories.Base.Interfaces;

public interface IBaseRepositoryAsync<T> where T : BaseEntity
{
    /// <summary>
    ///     Create a new record in database asynchronously.
    /// </summary>
    /// <param name="entity">new entity (Entity: <see cref="T" />)</param>
    /// <param name="cancellationToken"></param>
    public Task<Guid?> CreateAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Create new records in database asynchronously.
    /// </summary>
    /// <param name="entities">(Collection: <see cref="T" />)</param>
    /// <param name="cancellationToken"></param>
    public Task<ICollection<Guid>> CreateManyAsync(ICollection<T> entities,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Update record in database asynchronously.
    /// </summary>
    /// <param name="entity">Record to update.</param>
    /// <param name="cancellationToken"></param>
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Update records in database asynchronously.
    /// </summary>
    /// <param name="entities">Records to update.</param>
    /// <param name="cancellationToken"></param>
    public Task UpdateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Remove by change status to Delete asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"></param>
    public Task RemoveSoftAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public Task RemoveSoftAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously where.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    public Task RemoveSoftWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Remove record from database asynchronously.
    /// </summary>
    /// <param name="id">Record id.</param>
    /// <param name="cancellationToken"></param>
    public Task RemoveHardAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously.
    /// </summary>
    /// <param name="cancellationToken"></param>
    public Task RemoveHardAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Remove all records from database asynchronously where.
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    public Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Execute Raw SQL asynchronously.
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public Task<int> ExecuteSqlRawAsync(string sql,CancellationToken cancellationToken = default, params object[] parameters);
    
}