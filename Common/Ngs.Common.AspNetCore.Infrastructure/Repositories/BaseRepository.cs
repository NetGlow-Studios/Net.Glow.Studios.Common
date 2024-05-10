using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories;

/// <summary>
/// Base repository for entities
/// </summary>
/// <param name="applicationDbContext"> The application db context </param>
/// <typeparam name="T"> The entity type </typeparam>
[DebuggerStepThrough]
public abstract class BaseRepository<T>(DbContext applicationDbContext) : BaseRepositoryReadOnly<T>(applicationDbContext), IBaseRepository<T>  where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = applicationDbContext.Set<T>();
    private readonly DbContext _applicationDbContext = applicationDbContext;

    /// <summary>
    /// Create a new entity
    /// </summary>
    /// <param name="entity"> The entity to create </param>
    /// <returns> The id of the created entity </returns>
    public T Create(T entity)
    {
        try
        {
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.Status = StatusEnum.Active;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            _dbSet.Add(entity);
            _applicationDbContext.SaveChanges();

            return entity;   
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedException($"Resource ({nameof(T)}) not created", e);
        }
    }

    /// <summary>
    /// Create many entities
    /// </summary>
    /// <param name="entities"> The entities to create </param>
    /// <returns> The ids of the created entities </returns>
    public ICollection<T> CreateMany(ICollection<T> entities)
    {
        try
        {
            entities.ToList().ForEach(entity =>
            {
                entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
                entity.Status = StatusEnum.Active;
                entity.CreatedAt = DateTimeOffset.UtcNow;
                entity.UpdatedAt = DateTimeOffset.UtcNow;
            });

            _dbSet.AddRange(entities);
            _applicationDbContext.SaveChanges();

            return entities.ToList();
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedException($"Resource ({nameof(T)}) not created", e);
        }
    }

    /// <summary>
    /// Update an entity
    /// </summary>
    /// <param name="entity"> The entity to update </param>
    public void Update(T entity)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        _dbSet.Update(entity);
        _applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Update many entities
    /// </summary>
    /// <param name="entities"> The entities to update </param>
    public void UpdateMany(ICollection<T> entities)
    {
        entities.ToList().ForEach(x => x.UpdatedAt = DateTimeOffset.UtcNow);

        _dbSet.UpdateRange(entities);
        _applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Remove an entity. Soft remove updates the status of the entity to deleted
    /// </summary>
    /// <param name="id"> The id of the entity to remove </param>
    public void RemoveSoft(Guid id)
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
    public void RemoveSoftWhere(Expression<Func<T, bool>> predicate)
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
    public void RemoveHard(Guid id)
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
    public void RemoveHardWhere(Expression<Func<T, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);

        _dbSet.RemoveRange(entities);
        _applicationDbContext.SaveChanges();
    }
}