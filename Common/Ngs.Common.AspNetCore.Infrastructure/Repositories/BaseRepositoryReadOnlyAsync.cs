using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories;

[DebuggerStepThrough]
public abstract class BaseRepositoryReadOnlyAsync<TEntity>(DbContext applicationDbContext) : BaseRepositoryReadOnlyAsync<TEntity, Guid>(applicationDbContext) where TEntity : BaseEntity;

[DebuggerStepThrough]
public abstract class BaseRepositoryReadOnlyAsync<TEntity, TId>(DbContext applicationDbContext) : IBaseRepositoryReadOnlyAsync<TEntity, TId> where TEntity : BaseEntity<TId> where TId : struct, IEquatable<TId>
{
    private readonly DbSet<TEntity> _dbSet = applicationDbContext.Set<TEntity>();

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await CountAsync(entity => true, cancellationToken);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync(StatusEnum statusEnum, CancellationToken cancellationToken = default)
    {
        return await CountAsync(entity => entity.Status == statusEnum, cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllOrderedAsync(order => order.CreatedAt, cancellationToken, includeProperties);
    }
    
    public async Task<ICollection<TEntity>> GetAllAsync(StatusEnum status, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllOrderedAsync(status, order => order.CreatedAt, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllAsync(StatusEnum status, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllOrderedAsync(status, predicate, order => order.CreatedAt, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllOrderedAsync(predicate, order => order.CreatedAt, cancellationToken, includeProperties);
    }
    
    public async Task<ICollection<TEntity>> GetAllOrderedAsync(Expression<Func<TEntity, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllOrderedAsync(orderBy, true, cancellationToken, includeProperties);
    }
    
    public async Task<ICollection<TEntity>> GetAllOrderedAsync(Expression<Func<TEntity, object>> orderBy, bool ascending, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllOrderedAsync(x=>true, orderBy, ascending, cancellationToken, includeProperties);
    }
    
    public async Task<ICollection<TEntity>> GetAllOrderedAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllOrderedAsync(predicate, orderBy, true, cancellationToken, includeProperties);
    }
    
    public async Task<ICollection<TEntity>> GetAllOrderedAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, bool ascending, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));
        
        return await (ascending ? query.Where(predicate).OrderBy(orderBy) : query.Where(predicate).OrderByDescending(orderBy)).ToListAsync(cancellationToken);
    }
    
    public async Task<ICollection<TEntity>> GetAllOrderedAsync(StatusEnum status, Expression<Func<TEntity, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));
        
        return await query.Where(x => x.Status == status).OrderBy(orderBy).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAllOrderedAsync(StatusEnum status, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));
        
        return await query.Where(x => x.Status == status).Where(predicate).OrderBy(orderBy).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAllOrderedAsync(StatusEnum[] statuses, Expression<Func<TEntity, object>> orderBy, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));
        
        return await query.Where(x => statuses.Contains(x.Status)).OrderBy(orderBy).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetAllActiveAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Active, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllActiveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Active, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllDraftAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Draft, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllDraftAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Draft, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllHiddenAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Hidden, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllHiddenAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Hidden, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllInactiveAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Inactive, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllInactiveAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Inactive, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllOutdatedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Outdated, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllOutdatedAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Outdated, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllArchivedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Archived, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllArchivedAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Archived, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllSuspendedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Suspended, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllSuspendedAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Suspended, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllLockedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Locked, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllLockedAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.Locked, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllPendingActivationAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.PendingActivation, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllPendingActivationAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.PendingActivation, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllPendingDeactivationAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.PendingDeactivation, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetAllPendingDeactivationAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(StatusEnum.PendingDeactivation, predicate, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetWithoutStatusAsync(StatusEnum status, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(x=>x.Status != status, cancellationToken, includeProperties);
    }
    
    public async Task<ICollection<TEntity>> GetTopNAsync(int number, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetTopNAsync(x=>true, number, cancellationToken, includeProperties);
    }

    public async Task<ICollection<TEntity>> GetTopNAsync(StatusEnum status, int number, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetTopNAsync(x=>x.Status == status, number, cancellationToken, includeProperties);
    }
    
    public async Task<ICollection<TEntity>> GetTopNAsync(Expression<Func<TEntity, bool>> predicate, int number, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.Where(predicate).OrderBy(x=>x.CreatedAt).Take(number).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetByIdsAsync(IEnumerable<TId> ids, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetAllAsync(x=>ids.Contains(x.Id), cancellationToken, includeProperties);
    }

    public async Task<TEntity> GetFirstAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.OrderBy(x=>x.CreatedAt).FirstAsync(cancellationToken);
    }

    public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.FirstAsync(predicate, cancellationToken);
    }

    public async Task<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        try
        {
            return await GetFirstAsync(predicate, cancellationToken, includeProperties);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
    
    public async Task<TEntity?> GetLastAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetLastAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));

        return await query.LastAsync(predicate, cancellationToken);
    }

    public async Task<TEntity?> GetLastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        try
        {
            return await GetLastAsync(predicate, cancellationToken, includeProperties);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public async Task<TEntity?> GetLastUpdatedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.OrderBy(x => x.UpdatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> GetRandomAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var rand = new Random();

        var entities = (await GetAllAsync(cancellationToken)).ToList();

        return entities[rand.Next(entities.Count - 1)];
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public async Task<ICollection<TEntity>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        return await GetPageAsync(x=>true, page, pageSize, cancellationToken, includeProperties);
    }
    
    public async Task<ICollection<TEntity>> GetPageAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x => !string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));

        return await query
            .Where(predicate)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(predicate, cancellationToken);
    }

    public async Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AllAsync(predicate, cancellationToken);
    }

    public async Task<bool> IsWithIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public async Task<bool> IsActiveAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await HasStatusAsync(id, StatusEnum.Active, cancellationToken);
    }

    public async Task<bool> IsWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(x=> x.Status == status, cancellationToken);
    }
    
    public async Task<bool> HasStatusAsync(TId id, StatusEnum status, CancellationToken cancellationToken = default)
    {
        return await AnyAsync(x => x.Id.Equals(id) && x.Status == status, cancellationToken);
    }
}