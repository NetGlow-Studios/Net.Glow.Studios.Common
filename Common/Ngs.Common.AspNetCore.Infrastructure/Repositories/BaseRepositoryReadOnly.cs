using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories;

[DebuggerStepThrough]
public abstract class BaseRepositoryReadOnly<TEntity>(DbContext applicationDbContext) : BaseRepositoryReadOnly<TEntity, Guid>(applicationDbContext) where TEntity : BaseEntity;


[DebuggerStepThrough]
public abstract class BaseRepositoryReadOnly<TEntity, TId>(DbContext applicationDbContext) : IBaseRepositoryReadOnly<TEntity, TId> where TEntity : BaseEntity<TId> where TId : struct, IEquatable<TId>
{
    private readonly DbSet<TEntity> _dbSet = applicationDbContext.Set<TEntity>();
    
    public int Count()
    {
        return Count(x=>true);
    }

    public int Count(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Count(predicate);
    }

    public int Count(StatusEnum statusEnum)
    {
        return Count(x => x.Status == statusEnum);
    }

    public ICollection<TEntity> GetAll(params string[] includeProperties)
    {
        return GetAllOrdered(order => order.CreatedAt, includeProperties);
    }

    public ICollection<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAllOrdered(predicate, order => order.CreatedAt, includeProperties);
    }

    public ICollection<TEntity> GetAll(StatusEnum status, params string[] includeProperties)
    {
        return GetAllOrdered(status, order => order.CreatedAt, includeProperties);
    }

    public ICollection<TEntity> GetAll(StatusEnum status, Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAllOrdered(status, predicate, order => order.CreatedAt, includeProperties);
    }

    public ICollection<TEntity> GetAllOrdered(Expression<Func<TEntity, object>> orderBy, params string[] includeProperties)
    {
        return GetAllOrdered(orderBy, true, includeProperties);
    }

    public ICollection<TEntity> GetAllOrdered(Expression<Func<TEntity, object>> orderBy, bool ascending, params string[] includeProperties)
    {
        return GetAllOrdered(x => true, orderBy, ascending, includeProperties);
    }

    public ICollection<TEntity> GetAllOrdered(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, params string[] includeProperties)
    {
        return GetAllOrdered(predicate, orderBy, true, includeProperties);
    }

    public ICollection<TEntity> GetAllOrdered(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, bool ascending, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));
        
        return (ascending ? query.Where(predicate).OrderBy(orderBy) : query.Where(predicate).OrderByDescending(orderBy)).ToList();
    }

    public ICollection<TEntity> GetAllOrdered(StatusEnum status, Expression<Func<TEntity, object>> orderBy, params string[] includeProperties)
    {
        return GetAllOrdered(x=>x.Status == status, orderBy, includeProperties);
    }

    public ICollection<TEntity> GetAllOrdered(StatusEnum status, Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));
        
        return query.Where(x=>x.Status == status).Where(predicate).OrderBy(orderBy).ToList();
    }

    public async Task<ICollection<TEntity>> GetAllOrdered(StatusEnum[] statuses, Expression<Func<TEntity, object>> orderBy, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));
        
        return await query.Where(x=>statuses.Contains(x.Status)).OrderBy(orderBy).ToListAsync();
    }

    public ICollection<TEntity> GetActive(params string[] includeProperties)
    {
        return GetAll(StatusEnum.Active, includeProperties);
    }

    public ICollection<TEntity> GetActive(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.Active, predicate, includeProperties);
    }

    public ICollection<TEntity> GetDraft(params string[] includeProperties)
    {
        return GetAll(StatusEnum.Draft, includeProperties);
    }

    public ICollection<TEntity> GetDraft(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.Draft, predicate, includeProperties);
    }

    public ICollection<TEntity> GetHidden(params string[] includeProperties)
    {
        return GetAll(StatusEnum.Hidden, includeProperties);
    }

    public ICollection<TEntity> GetHidden(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.Hidden, predicate, includeProperties);
    }

    public ICollection<TEntity> GetInactive(params string[] includeProperties)
    {
        return GetAll(StatusEnum.Inactive, includeProperties);
    }

    public ICollection<TEntity> GetInactive(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.Inactive, predicate, includeProperties);
    }

    public ICollection<TEntity> GetOutdated(params string[] includeProperties)
    {
        return GetAll(StatusEnum.Outdated, includeProperties);
    }

    public ICollection<TEntity> GetOutdated(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.Outdated, predicate, includeProperties);
    }

    public ICollection<TEntity> GetArchived(params string[] includeProperties)
    {
        return GetAll(StatusEnum.Archived, includeProperties);
    }

    public ICollection<TEntity> GetArchived(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.Archived, predicate, includeProperties);
    }

    public ICollection<TEntity> GetSuspended(params string[] includeProperties)
    {
        return GetAll(StatusEnum.Suspended, includeProperties);
    }

    public ICollection<TEntity> GetSuspended(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.Suspended, predicate, includeProperties);
    }

    public ICollection<TEntity> GetLocked(params string[] includeProperties)
    {
        return GetAll(StatusEnum.Locked, includeProperties);
    }

    public ICollection<TEntity> GetLocked(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.Locked, predicate, includeProperties);
    }

    public ICollection<TEntity> GetPendingActivation(params string[] includeProperties)
    {
        return GetAll(StatusEnum.PendingActivation, includeProperties);
    }

    public ICollection<TEntity> GetPendingActivation(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.PendingActivation, predicate, includeProperties);
    }

    public ICollection<TEntity> GetPendingDeactivation(params string[] includeProperties)
    {
        return GetAll(StatusEnum.PendingDeactivation, includeProperties);
    }

    public ICollection<TEntity> GetPendingDeactivation(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        return GetAll(StatusEnum.PendingDeactivation, predicate, includeProperties);
    }

    public ICollection<TEntity> GetWithoutStatus(StatusEnum status, params string[] includeProperties)
    {
        return GetAll(x => x.Status != status, includeProperties);
    }

    public ICollection<TEntity> GetTopN(int number, params string[] includeProperties)
    {
        return GetTopN(x => true, number, includeProperties);
    }

    public ICollection<TEntity> GetTopN(StatusEnum status, int number, params string[] includeProperties)
    {
        return GetTopN(x => x.Status == status, number, includeProperties);
    }

    public ICollection<TEntity> GetTopN(Expression<Func<TEntity, bool>> predicate, int number, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty) 
                    => current.Include(includeProperty));
        
        return query.Where(predicate).Take(number).ToList();
    }

    public ICollection<TEntity> GetByIds(IEnumerable<TId> ids, params string[] includeProperties)
    {
        return GetAll(x => ids.Contains(x.Id), includeProperties);
    }

    public TEntity? GetById(TId id, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));
        
        return query.SingleOrDefault(x => x.Id.Equals(id));
    }

    public TEntity GetFirst(params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));

        return  query.OrderBy(x=>x.CreatedAt).First();
    }

    public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));
        
        return query.First(predicate);
    }

    public TEntity? GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        try
        {
            return GetFirst(predicate, includeProperties);
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }

    public TEntity? GetLast(params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));
        
        return query.OrderByDescending(x=>x.CreatedAt).FirstOrDefault();
    }

    public TEntity GetLast(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));
        
        return query.OrderByDescending(x=>x.CreatedAt).First(predicate);
    }

    public TEntity? GetLastOrDefault(Expression<Func<TEntity, bool>> predicate, params string[] includeProperties)
    {
        try
        {
            return GetLast(predicate, includeProperties);
        }
        catch(InvalidOperationException)
        {
            return null;
        }
    }

    public TEntity? GetLastUpdated(params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));
        
        return query.OrderByDescending(x=>x.UpdatedAt).FirstOrDefault();
    }

    public TEntity GetRandom(params string[] includeProperties)
    {
        var random = new Random();
        
        var entities = GetAll().ToList();
        
        return entities[random.Next(entities.Count)];
    }

    public ICollection<TEntity> GetPage(int page, int pageSize, params string[] includeProperties)
    {
        return GetPage(x=>true, page, pageSize, includeProperties);
    }

    public ICollection<TEntity> GetPage(Expression<Func<TEntity, bool>> predicate, int page, int pageSize, params string[] includeProperties)
    {
        var query = includeProperties
            .Where(x=>!string.IsNullOrEmpty(x))
            .Aggregate<string, IQueryable<TEntity>>(
                _dbSet, (current, includeProperty)
                    => current.Include(includeProperty));
        
        return query
            .Where(predicate)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    public bool All(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.All(predicate);
    }

    public bool IsWithId(TId id)
    {
        return Any(x => x.Id.Equals(id));
    }

    public bool IsActive(TId id)
    {
        return HasStatus(id, StatusEnum.Active);
    }

    public bool IsWithStatus(StatusEnum status)
    {
        return Any(x => x.Status == status);
    }

    public bool HasStatus(TId id, StatusEnum status)
    {
        return Any(x => x.Id.Equals(id) && x.Status == status);
    }
}