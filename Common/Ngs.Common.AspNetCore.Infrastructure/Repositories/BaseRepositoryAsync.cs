using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories;

/// <summary>
/// Base repository for all repositories that are async
/// </summary>
/// <param name="applicationDbContext"> The application db context </param>
/// <typeparam name="TEntity"> The entity type </typeparam>
[DebuggerStepThrough]
public abstract class BaseRepositoryAsync<TEntity>(DbContext applicationDbContext) : BaseRepositoryAsync<TEntity, Guid>(applicationDbContext) where TEntity : BaseEntity;

/// <summary>
/// Base repository for all repositories that are async
/// </summary>
/// <param name="applicationDbContext"> The application db context </param>
/// <typeparam name="TEntity"> The entity type </typeparam>
/// <typeparam name="TId"> The id type </typeparam>
[DebuggerStepThrough]
public abstract class BaseRepositoryAsync<TEntity, TId>(DbContext applicationDbContext) : BaseRepositoryReadOnlyAsync<TEntity, TId>(applicationDbContext)
    where TEntity : BaseEntity<TId> 
    where TId : struct, IEquatable<TId>
{
    private readonly DbSet<TEntity> _dbSet = applicationDbContext.Set<TEntity>();
    private readonly DbContext _applicationDbContext = applicationDbContext;
    
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.Status = entity.Status != StatusEnum.Active ? entity.Status : StatusEnum.Active;
            entity.CreatedAt = DateTimeOffset.Now;
            entity.UpdatedAt = DateTimeOffset.Now;
    
            await _dbSet.AddAsync(entity, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
    
            return entity;
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedException($"Entity ({typeof(TEntity).FullName}) not created", e);
        }
    }
    
    public async Task<ICollection<TEntity>> CreateManyAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var entity in entities)
            {
                entity.Status = entity.Status != StatusEnum.Active ? entity.Status : StatusEnum.Active;
                entity.CreatedAt = DateTimeOffset.Now;
                entity.UpdatedAt = DateTimeOffset.Now;
            }
    
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
    
            return entities;
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedException($"Entities ({typeof(TEntity).FullName}) not created", e);
        }
    }
    
    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTimeOffset.Now;
    
        _dbSet.Update(entity);
        
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }
    
    public async Task<ICollection<TEntity>> UpdateManyAsync(ICollection<TEntity> entities, CancellationToken cancellationToken = default)
    {
        entities.ToList().ForEach(x => x.UpdatedAt = DateTimeOffset.Now);
    
        _dbSet.UpdateRange(entities);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return entities;
    }
    
    public async Task UpdateStatusAsync(TEntity entity, StatusEnum status, CancellationToken cancellationToken = default)
    {
        entity.Status = status;
    
        await UpdateAsync(entity, cancellationToken);
    }
    
    public async Task UpdateStatusAsync(TId id, StatusEnum status, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
    
        if (entity is null)
        {
            throw new EntityNotFoundException(id, typeof(TEntity).FullName ?? string.Empty, $"Status cannot be updated to: {Enum.GetName(status)}");
        }
    
        entity.Status = status;
    
        await UpdateAsync(entity, cancellationToken);
    }
    
    public async Task RemoveSoftAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
    
        if (entity == null)
        {
            throw new EntityNotFoundException("Entity not found. Cannot be removed.");
        }
    
        await UpdateStatusAsync(entity, StatusEnum.Deleted, cancellationToken);
    }
    
    public async Task RemoveSoftAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await GetAllAsync(cancellationToken);
    
        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }
    
        await UpdateManyAsync(entities.ToList(), cancellationToken);
    }
    
    public async Task RemoveSoftWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entities = _dbSet.Where(predicate);
    
        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }
    
        await UpdateManyAsync(await entities.ToListAsync(cancellationToken), cancellationToken);
    }
    
    public async Task RemoveHardAsync(TId id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
    
        if (entity == null)
        {
            throw new EntityNotFoundException("Entity not found. Cannot be removed.");
        }
        
        _dbSet.Remove(entity);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task RemoveHardWhereAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);
    
        _dbSet.RemoveRange(entities);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<TEntity> UpdateAdditionalInfoAsync(TEntity entity, string info, CancellationToken cancellationToken = default)
    {
        entity.AdditionalInformation = info;
    
        await UpdateAsync(entity, cancellationToken);
    
        return entity;
    }
    
    public async Task<TEntity> UpdateAdditionalInfoAsync(TId id, string info, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
    
        if (entity is null)
        {
            throw new EntityNotFoundException(id, typeof(TEntity).FullName ?? string.Empty, "Additional information cannot be updated.");
        }
    
        entity.AdditionalInformation = info;
    
        await UpdateAsync(entity, cancellationToken);
    
        return entity;
    }
    
    public async Task<TEntity> AddTagAsync(TEntity entity, string tag, CancellationToken cancellationToken = default)
    {
        return await AddTagAsync(entity.Id, tag, cancellationToken);
    }
    
    public async Task<TEntity> AddTagAsync(TId id, string tag, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
    
        if (entity is null)
        {
            throw new EntityNotFoundException(id, typeof(TEntity).FullName ?? string.Empty, "Tag cannot be added.");
        }
    
        entity.AddTag(tag);
    
        await UpdateAsync(entity, cancellationToken);
    
        return entity;
    }
    
    public async Task<TEntity> RemoveTagAsync(TEntity entity, string tag, CancellationToken cancellationToken = default)
    {
        return await RemoveTagAsync(entity.Id, tag, cancellationToken);
    }
    
    public async Task<TEntity> RemoveTagAsync(TId id, string tag, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
    
        if (entity is null)
        {
            throw new EntityNotFoundException(id, typeof(TEntity).FullName ?? string.Empty, "Tag cannot be removed.");
        }
    
        entity.RemoveTag(tag);
    
        await UpdateAsync(entity, cancellationToken);
    
        return entity;
    }
    
    public async Task<bool> HasTagAsync(TEntity entity, string tag, CancellationToken cancellationToken = default)
    {
        return await HasTagAsync(entity.Id, tag, cancellationToken);
    }
    
    public async Task<bool> HasTagAsync(TId id, string tag, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
    
        if (entity is null)
        {
            throw new EntityNotFoundException(id, typeof(TEntity).FullName ?? string.Empty, "Tag cannot be checked.");
        }
    
        return entity.Tags.Contains(tag);
    }
}