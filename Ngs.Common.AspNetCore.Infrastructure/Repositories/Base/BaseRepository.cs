using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Entities.Base;
using Ngs.Common.AspNetCore.Enums.Base;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Base.Interfaces;

namespace Ngs.Common.AspNetCore.Infrastructure.Repositories.Base;

public abstract class BaseRepository<T>(DbContext applicationDbContext) : IBaseRepository<T>, IBaseRepositoryReadOnly<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet = applicationDbContext.Set<T>();

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

    public int Count()
    {
        return _dbSet.Count();
    }

    public int CountWhere(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate).Count();
    }

    public int CountByStatus(StatusEnum statusEnum)
    {
        return CountWhere(x => x.Status == statusEnum);
    }

    public ICollection<T> GetAll(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.ToList();
    }

    public ICollection<T> GetAllWhere(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(predicate).ToList();
    }

    public ICollection<T> GetWithStatus(StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(x => x.Status == status).ToList();
    }

    public ICollection<T> GetWithoutStatus(StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(x => x.Status != status).ToList();
    }

    public ICollection<T> GetTopNByStatus(int n, StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(x => x.Status == status).Take(n).ToList();
    }

    public ICollection<T> GetByIds(IEnumerable<Guid> ids, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Where(x => ids.Contains(x.Id)).ToList();
    }

    public T GetFirst(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.First(predicate);
    }

    public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.FirstOrDefault(predicate);
    }

    public T GetLast(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.Last(predicate);
    }

    public T? GetLastOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.LastOrDefault(predicate);
    }

    public T? GetLastCreated(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.OrderBy(x => x.CreatedAt).FirstOrDefault();
    }

    public T? GetLastUpdated(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.OrderBy(x => x.UpdatedAt).FirstOrDefault();
    }

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

    public T? GetById(Guid id, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return query.FirstOrDefault(x => x.Id == id);
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        applicationDbContext.SaveChanges();
    }

    public void UpdateMany(ICollection<T> entities)
    {
        entities.ToList().ForEach(x => x.UpdatedAt = DateTime.UtcNow);

        _dbSet.UpdateRange(entities);
        applicationDbContext.SaveChanges();
    }

    public void RemoveSoft(Guid id)
    {
        var entity = GetById(id);

        if (entity != null)
        {
            entity.Status = StatusEnum.Deleted;

            Update(entity);
        }
    }

    public void RemoveSoftAll()
    {
        var entities = _dbSet.ToList();

        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }

        UpdateMany(entities);
    }

    public void RemoveSoftWhere(Expression<Func<T, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);

        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }

        UpdateMany(entities.ToList());
    }

    public void RemoveHard(Guid id)
    {
        var entity = GetById(id);

        if (entity == null) return;

        _dbSet.Remove(entity);
        applicationDbContext.SaveChanges();
    }

    public void RemoveHardAll()
    {
        _dbSet.RemoveRange(GetAll());
        applicationDbContext.SaveChanges();
    }

    public void RemoveHardWhere(Expression<Func<T, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);

        _dbSet.RemoveRange(entities);
        applicationDbContext.SaveChanges();
    }

    public bool Any(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    public bool All(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.All(predicate);
    }

    public bool IsWithId(Guid id)
    {
        return GetById(id) != null;
    }

    public bool IsActive(Guid id)
    {
        return GetById(id)?.Status == StatusEnum.Active;
    }

    public bool IsWithStatus(StatusEnum status)
    {
        return GetAll().Any(x => x.Status == status);
    }
}