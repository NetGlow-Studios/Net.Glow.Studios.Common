using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories;

/// <summary>
/// Base repository for entities
/// </summary>
/// <param name="applicationDbContext"> The application db context </param>
/// <typeparam name="T"> The entity type </typeparam>
public abstract class BaseRepository<T>(DbContext applicationDbContext) : IBaseRepository<T>, IBaseRepositoryReadOnly<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = applicationDbContext.Set<T>();

    /// <summary>
    /// Create a new entity
    /// </summary>
    /// <param name="entity"> The entity to create </param>
    /// <returns> The id of the created entity </returns>
    public Guid? Create(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.Status = StatusEnum.Active;
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Add(entity);
        applicationDbContext.SaveChanges();

        return entity.Id;
    }
    
    /// <summary>
    /// Create many entities
    /// </summary>
    /// <param name="entities"> The entities to create </param>
    /// <returns> The ids of the created entities </returns>
    public ICollection<Guid> CreateMany(ICollection<T> entities)
    {
        var ids = new List<Guid>();
        entities.ToList().ForEach(entity =>
        {
            entity.Id = Guid.NewGuid();
            entity.Status = StatusEnum.Active;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            ids.Add(entity.Id);
        });

        _dbSet.AddRange(entities);
        applicationDbContext.SaveChanges();

        return ids;
    }

    /// <summary>
    /// Count the number of entities
    /// </summary>
    /// <returns> The number of entities </returns>
    public int Count()
    {
        return _dbSet.Count();
    }

    /// <summary>
    /// Count the number of entities that satisfy a condition
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public int CountWhere(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate).Count();
    }

    /// <summary>
    /// Count the number of entities with a specific status
    /// </summary>
    /// <param name="statusEnum"> The status of the entities </param>
    /// <returns> The number of entities </returns>
    public int CountByStatus(StatusEnum statusEnum)
    {
        return CountWhere(x => x.Status == statusEnum);
    }

    /// <summary>
    /// Count all the entities
    /// </summary>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The number of entities </returns>
    public ICollection<T> GetAll(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.ToList();
    }

    /// <summary>
    /// Get all the entities that satisfy a condition
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The entities that satisfy the condition </returns>
    public ICollection<T> GetAllWhere(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(predicate).ToList();
    }

    /// <summary>
    /// Get all the entities with a specific status
    /// </summary>
    /// <param name="status"> The status of the entities </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The entities with the specific status </returns>
    public ICollection<T> GetWithStatus(StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(x => x.Status == status).ToList();
    }

    /// <summary>
    /// Get all the entities without a specific status
    /// </summary>
    /// <param name="status"> The status of the entities </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The entities without the specific status </returns>
    public ICollection<T> GetWithoutStatus(StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(x => x.Status != status).ToList();
    }

    /// <summary>
    /// Get the top n entities
    /// </summary>
    /// <param name="n"> The number of entities to get </param>
    /// <param name="status"> The status of the entities </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The entities with the specific status </returns>
    public ICollection<T> GetTopNByStatus(int n, StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(x => x.Status == status).Take(n).ToList();
    }

    /// <summary>
    /// Get entities by their ids with specific properties
    /// </summary>
    /// <param name="ids"> The ids of the entities to get </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The entities without the specific status </returns>
    public ICollection<T> GetByIds(IEnumerable<Guid> ids, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(x => ids.Contains(x.Id)).ToList();
    }

    /// <summary>
    /// Get first entity that satisfy a condition
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The first entity that satisfy the condition </returns>
    public T GetFirst(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.First(predicate);
    }

    /// <summary>
    /// Get first entity that satisfy a condition or null
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The first entity that satisfy the condition or null </returns>
    public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.FirstOrDefault(predicate);
    }

    /// <summary>
    /// Get last entity that satisfy a condition
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The last entity that satisfy the condition </returns>
    public T GetLast(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Last(predicate);
    }

    /// <summary>
    /// Get last entity that satisfy a condition or null
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The last entity that satisfy the condition or null </returns>
    public T? GetLastOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.LastOrDefault(predicate);
    }

    /// <summary>
    /// Get last created entity
    /// </summary>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The last created entity </returns>
    public T? GetLastCreated(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.OrderBy(x => x.CreatedAt).FirstOrDefault();
    }

    /// <summary>
    /// Get last updated entity
    /// </summary>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The last updated entity </returns>
    public T? GetLastUpdated(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.OrderBy(x => x.UpdatedAt).FirstOrDefault();
    }

    /// <summary>
    /// Get random entity
    /// </summary>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns> The random entity </returns>
    public T GetRandom(params string[] includeProperties)
    {
        var rand = new Random();

        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        var entities = query.ToList();

        return entities[rand.Next(entities.Count - 1)];
    }

    /// <summary>
    /// Get entity by id
    /// </summary>
    /// <param name="id"> The id of the entity to get </param>
    /// <param name="includeProperties"> The properties to include related entities </param>
    /// <returns></returns>
    public T? GetById(Guid id, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.FirstOrDefault(x => x.Id == id);
    }

    /// <summary>
    /// Update an entity
    /// </summary>
    /// <param name="entity"> The entity to update </param>
    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Update many entities
    /// </summary>
    /// <param name="entities"> The entities to update </param>
    public void UpdateMany(ICollection<T> entities)
    {
        entities.ToList().ForEach(x => x.UpdatedAt = DateTime.UtcNow);

        _dbSet.UpdateRange(entities);
        applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Remove an entity. Soft remove updates the status of the entity to deleted
    /// </summary>
    /// <param name="id"> The id of the entity to remove </param>
    public void RemoveSoft(Guid id)
    {
        var entity = GetById(id);

        if (entity == null) return;
        
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

        if (entity == null) return;

        _dbSet.Remove(entity);
        applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Remove entities if they satisfy a condition. Hard remove deletes the entities from the database
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    public void RemoveHardWhere(Expression<Func<T, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);

        _dbSet.RemoveRange(entities);
        applicationDbContext.SaveChanges();
    }

    /// <summary>
    /// Check if any entity satisfy a condition
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    /// <returns> True if any entity satisfy the condition, false otherwise </returns>
    public bool Any(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    /// <summary>
    /// Check if all entities satisfy a condition
    /// </summary>
    /// <param name="predicate"> The condition to satisfy </param>
    /// <returns> True if all entities satisfy the condition, false otherwise </returns>
    public bool All(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.All(predicate);
    }

    /// <summary>
    /// Check if an entity with a specific id exists
    /// </summary>
    /// <param name="id"> The id of the entity </param>
    /// <returns> True if the entity exists, false otherwise </returns>
    public bool IsWithId(Guid id)
    {
        return GetById(id) != null;
    }

    /// <summary>
    /// Check if an entity with a specific id is active
    /// </summary>
    /// <param name="id"> The id of the entity </param>
    /// <returns> True if the entity is active, false otherwise </returns>
    public bool IsActive(Guid id)
    {
        return GetById(id)?.Status == StatusEnum.Active;
    }

    /// <summary>
    /// Check if an entity with a specific id has specific status
    /// </summary>
    /// <param name="status"> The status of the entity </param>
    /// <returns> True if the entity has the specific status, false otherwise </returns>
    public bool IsWithStatus(StatusEnum status)
    {
        return GetAll().Any(x => x.Status == status);
    }
}