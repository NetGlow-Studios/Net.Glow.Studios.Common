using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories;

[DebuggerStepThrough]
public abstract class BaseRepositoryReadOnlyAsync<T>(DbContext applicationDbContext) : IBaseRepositoryReadOnlyAsync<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = applicationDbContext.Set<T>();

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

    public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));

        return await query.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));

        return await query.Where(x => x.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetWithoutStatusAsync(StatusEnum status, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.Where(x => x.Status != status).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetTopNByStatusAsync(int n, StatusEnum status, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.Where(x => x.Status == status).Take(n).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }

    public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.FirstAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));

        return await query.LastAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.LastOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetLastCreatedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetLastUpdatedAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.OrderBy(x => x.UpdatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T> GetRandomAsync(CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var rand = new Random();

        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        var entities = await query.ToListAsync(cancellationToken);

        return entities[rand.Next(entities.Count - 1)];
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<ICollection<T>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));
        
        return await query.Skip(page * pageSize).Take(pageSize).ToListAsync(cancellationToken);
    }
    
    public async Task<ICollection<T>> GetPageWhereAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x => !string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<T>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));

        return await query
            .Where(predicate)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
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
        return await _dbSet.AnyAsync(x => x.Id.Equals(id), cancellationToken);
    }

    public async Task<bool> IsActiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);

        return entity?.Status == StatusEnum.Active;
    }

    public async Task<bool> IsWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AnyAsync(x=> x.Status == status, cancellationToken);
    }
}