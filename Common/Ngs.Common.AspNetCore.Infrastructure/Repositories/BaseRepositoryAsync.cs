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
public abstract class BaseRepositoryAsync<T>(DbContext applicationDbContext) : IBaseRepositoryAsync<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = applicationDbContext.Set<T>();

    public async Task<T?> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.Status = StatusEnum.Active;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            await _dbSet.AddAsync(entity, cancellationToken);
            await applicationDbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedRepositoryException($"Resource ({nameof(T)}) not created", e);
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
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                newEntities.Add(entity);
            }
            catch (Exception e)
            {
                throw new EntityNotCreatedRepositoryException($"Resource ({nameof(T)}) not created", e);
            }
        });

        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await applicationDbContext.SaveChangesAsync(cancellationToken);

        return newEntities;
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    public async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(predicate, cancellationToken: cancellationToken);
    }

    public async Task<int> CountByStatusAsync(StatusEnum statusEnum, CancellationToken cancellationToken = default)
    {
        return await CountWhereAsync(x => x.Status == statusEnum, cancellationToken);
    }

    public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetWithStatusAsync(StatusEnum status,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(x => x.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetWithoutStatusAsync(StatusEnum status, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(x => x.Status != status).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetTopNByStatusAsync(int n, StatusEnum status,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(x => x.Status == status).Take(n).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetByIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }

    public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.FirstAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.LastAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.LastOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetLastCreatedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetLastUpdatedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.OrderBy(x => x.UpdatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetRandomAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var rand = new Random();

        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        var entities = await query.ToListAsync(cancellationToken);

        return entities[rand.Next(entities.Count - 1)];
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ICollection<T>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => current.Include(includeProperty!));

        return await query.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetPageWhereAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        includeProperties = includeProperties.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => current.Include(includeProperty!));

        return await query.Where(predicate).Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public async Task<ICollection<T>> UpdateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        entities.ToList().ForEach(x => x.UpdatedAt = DateTime.UtcNow);

        _dbSet.UpdateRange(entities);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        
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

        if (entity == null)
        {
            throw new EntityNotFoundRepositoryException(id, typeof(T).FullName ?? string.Empty, $"Status cannot be updated to: {Enum.GetName(status)}");
        }

        entity.Status = status;

        await UpdateAsync(entity, cancellationToken);
    }

    public async Task RemoveSoftAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        if (entity != null)
        {
            entity.Status = StatusEnum.Deleted;

            await UpdateAsync(entity, cancellationToken);
        }
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

    public async Task RemoveSoftWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
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

        if (entity != null)
        {
            _dbSet.Remove(entity);
            await applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RemoveHardAllAsync(CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(await GetAllAsync(cancellationToken));
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        _dbSet.RemoveRange(entities);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AllAsync(predicate, cancellationToken);
    }

    public async Task<bool> IsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        return entity != null;
    }

    public async Task<bool> IsActiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        return entity?.Status == StatusEnum.Active;
    }

    public async Task<bool> IsWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default)
    {
        var entities = await GetAllAsync(cancellationToken);

        return entities.Any(x => x.Status == status);
    }
}