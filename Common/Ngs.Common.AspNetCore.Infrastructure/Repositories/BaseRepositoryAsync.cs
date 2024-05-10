using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories;

/// <summary>
/// Base repository for all repositories that are async
/// </summary>
/// <param name="applicationDbContext"> The application db context </param>
/// <typeparam name="T"> The entity type </typeparam>
[DebuggerStepThrough]
public abstract class BaseRepositoryAsync<T>(DbContext applicationDbContext) : BaseRepositoryReadOnlyAsync<T>(applicationDbContext), IBaseRepositoryAsync<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = applicationDbContext.Set<T>();
    private readonly DbContext _applicationDbContext = applicationDbContext;

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.Status = StatusEnum.Active;
            entity.CreatedAt = DateTimeOffset.UtcNow;
            entity.UpdatedAt = DateTimeOffset.UtcNow;

            await _dbSet.AddAsync(entity, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedException($"Entity ({nameof(T)}) not created", e);
        }
    }

    public async Task<ICollection<T>> CreateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        var newEntities = new List<T>();

        entities.ToList().ForEach(entity =>
        {
            try
            {
                entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
                entity.Status = StatusEnum.Active;
                entity.CreatedAt = DateTimeOffset.UtcNow;
                entity.UpdatedAt = DateTimeOffset.UtcNow;
                newEntities.Add(entity);
            }
            catch (Exception e)
            {
                throw new EntityNotCreatedException($"Entities ({nameof(T)}) not created", e);
            }
        });

        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return newEntities;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;

        _dbSet.Update(entity);
        
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public async Task<ICollection<T>> UpdateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        entities.ToList().ForEach(x => x.UpdatedAt = DateTimeOffset.UtcNow);

        _dbSet.UpdateRange(entities);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return entities;
    }
    
    public async Task UpdateStatusAsync(T entity, StatusEnum status, CancellationToken cancellationToken = default)
    {
        entity.Status = status;

        await UpdateAsync(entity, cancellationToken);
    }
    
    public async Task UpdateStatusAsync(Guid id, StatusEnum status, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            throw new EntityNotFoundException(id, typeof(T).FullName ?? string.Empty, $"Status cannot be updated to: {Enum.GetName(status)}");
        }

        entity.Status = status;

        await UpdateAsync(entity, cancellationToken);
    }

    public async Task RemoveSoftAsync(Guid id, CancellationToken cancellationToken = default)
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

    public async Task RemoveSoftWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entities = _dbSet.Where(predicate);

        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }

        await UpdateManyAsync(await entities.ToListAsync(cancellationToken), cancellationToken);
    }

    public async Task RemoveHardAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        if (entity == null)
        {
            throw new EntityNotFoundException("Entity not found. Cannot be removed.");
        }
        
        _dbSet.Remove(entity);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        _dbSet.RemoveRange(entities);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }
}