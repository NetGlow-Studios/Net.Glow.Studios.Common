using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories;

/// <summary>
/// Base repository for entities
/// </summary>
/// <param name="applicationDbContext"> The application db context </param>
/// <typeparam name="TEntity"> The entity type </typeparam>
[DebuggerStepThrough]
public abstract class BaseRepository<TEntity>(DbContext applicationDbContext) : BaseRepository<TEntity, Guid>(applicationDbContext) where TEntity : BaseEntity;

/// <summary>
/// Base repository for entities
/// </summary>
/// <param name="applicationDbContext"> The application db context </param>
/// <typeparam name="TEntity"> The entity type </typeparam>
/// <typeparam name="TId"> The id type </typeparam>
[DebuggerStepThrough]
public abstract class BaseRepository<TEntity, TId>(DbContext applicationDbContext) : BaseRepositoryReadOnly<TEntity, TId>(applicationDbContext)
    where TEntity : BaseEntity<TId> 
    where TId : struct, IEquatable<TId>
{
    private readonly DbSet<TEntity> _dbSet = applicationDbContext.Set<TEntity>();
    private readonly DbContext _applicationDbContext = applicationDbContext;

    /// <summary>
    /// Create a new entity
    /// </summary>
    /// <param name="entity"> The entity to create </param>
    /// <returns> The id of the created entity </returns>
    public TEntity Create(TEntity entity)
    {
        try
        {
            entity.Status = entity.Status != StatusEnum.Active ? entity.Status : StatusEnum.Active;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            _dbSet.Add(entity);
            _applicationDbContext.SaveChanges();

            return entity;   
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedException($"Resource ({typeof(TEntity).FullName}) not created", e);
        }
    }

    /// <summary>
    /// Create many entities
    /// </summary>
    /// <param name="entities"> The entities to create </param>
    /// <returns> The ids of the created entities </returns>
    public ICollection<TEntity> CreateMany(ICollection<TEntity> entities)
    {
        try
        {
            foreach (var entity in entities)
            {
                entity.Status = entity.Status != StatusEnum.Active ? entity.Status : StatusEnum.Active;
                entity.CreatedAt = DateTimeOffset.UtcNow;
                entity.UpdatedAt = DateTimeOffset.UtcNow;
            }

            _dbSet.AddRange(entities);
            _applicationDbContext.SaveChanges();

            return entities.ToList();
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedException($"Resource ({typeof(TEntity).FullName}) not created", e);
        }
    }

    /// <summary>
    /// Update an entity
    /// </summary>
    /// <param name="entity"> The entity to update </param>
    public void Update(TEntity entity)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        _dbSet.Update(entity);
        _applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Update many entities
    /// </summary>
    /// <param name="entities"> The entities to update </param>
    public void UpdateMany(ICollection<TEntity> entities)
    {
        entities.ToList().ForEach(x => x.UpdatedAt = DateTimeOffset.UtcNow);

        _dbSet.UpdateRange(entities);
        _applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Remove an entity. Soft remove updates the status of the entity to deleted
    /// </summary>
    /// <param name="id"> The id of the entity to remove </param>
    public void RemoveSoft(TId id)
    {
        var entity = GetById(id);

        if (entity is null) return;
        
        entity.Status = StatusEnum.Deleted;

        Update(entity);
    }

    /// <summary>
    /// Remove all entities. Soft remove updates the status of the entities to deleted
    /// </summary>
    public void RemoveSoftAll()
    {
        var entities = _dbSet.ToList();

        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }

        UpdateMany(entities);
    }

    /// <summary>
    /// Remove all entities that satisfy a condition. Soft remove updates the status of the entities to deleted
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    public void RemoveSoftWhere(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);

        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }

        UpdateMany(entities.ToList());
    }

    /// <summary>
    /// Remove an entity. Hard remove deletes the entity from the database
    /// </summary>
    /// <param name="id"> The id of the entity to remove </param>
    public void RemoveHard(TId id)
    {
        var entity = GetById(id);

        if (entity is null) return;

        _dbSet.Remove(entity);
        _applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Remove entities if they satisfy a condition. Hard remove deletes the entities from the database
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    public void RemoveHardWhere(Expression<Func<TEntity, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);

        _dbSet.RemoveRange(entities);
        _applicationDbContext.SaveChanges();
    }

    public TEntity UpdateAdditionalInfo(TEntity entity, string info)
    {
        return UpdateAdditionalInfo(entity.Id, info); 
    }

    public TEntity UpdateAdditionalInfo(TId id, string info)
    {
        var entity = GetById(id);

        if (entity is null)
        {
            throw new EntityNotFoundException("Entity not found. Additional info cannot be updated.");
        }

        entity.AdditionalInformation = info;

        Update(entity);

        return entity;
    }

    public TEntity AddTag(TEntity entity, string tag)
    {
        return AddTag(entity.Id, tag);
    }

    public TEntity AddTag(TId id, string tag)
    {
        var entity = GetById(id);

        if (entity is null)
        {
            throw new EntityNotFoundException("Entity not found. Tag cannot be added.");
        }

        entity.AddTag(tag);

        Update(entity);

        return entity;
    }

    public TEntity RemoveTag(TEntity entity, string tag)
    {
        return RemoveTag(entity.Id, tag);
    }

    public TEntity RemoveTag(TId id, string tag)
    {
        var entity = GetById(id);

        if (entity is null)
        {
            throw new EntityNotFoundException("Entity not found. Tag cannot be removed.");
        }

        entity.RemoveTag(tag);

        Update(entity);

        return entity;
    }

    public bool HasTag(TEntity entity, string tag)
    {
        return HasTag(entity.Id, tag);
    }

    public bool HasTag(TId id, string tag)
    {
        var entity = GetById(id);

        if (entity is null)
        {
            throw new EntityNotFoundException("Entity not found. Tag cannot be checked.");
        }

        return entity.Tags.Contains(tag);
    }
}