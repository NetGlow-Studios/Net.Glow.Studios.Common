using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Net.Glow.Studios.Core.Entities.Base;
using Net.Glow.Studios.Core.Enums.Base;
using Net.Glow.Studios.Database.DBContexts;
using Net.Glow.Studios.Database.Repositories.Base.Interfaces;

namespace Net.Glow.Studios.Database.Repositories.Base;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly ApplicationDbContext _applicationDbContext;

    protected BaseRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Set<T>();
    }

    public async Task<Guid?> CreateAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.Status = StatusEnum.Active;
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        await _dbSet.AddAsync(entity);
        await _applicationDbContext.SaveChangesAsync();

        return entity.Id;
    }

    public Guid? Create(T entity)
    {
        entity.Id = Guid.NewGuid();
        entity.Status = StatusEnum.Active;
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Add(entity);
        _applicationDbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<ICollection<Guid>> CreateManyAsync(ICollection<T> entities)
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

        await _dbSet.AddRangeAsync(entities);
        await _applicationDbContext.SaveChangesAsync();

        return ids;
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
        _applicationDbContext.SaveChanges();
        
        return ids;
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync();

        return entities.Count;
    }

    public async Task<int> CountByStatusAsync(StatusEnum statusEnum)
    {
        return await CountWhereAsync(x => x.Status == statusEnum);
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

    public async Task<ICollection<T>> GetAllAsync(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.ToListAsync();
    }

    public ICollection<T> GetAll(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));
        
        return query.ToList();
    }

    public async Task<ICollection<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));
        
        return await query.Where(predicate).ToListAsync();
    }

    public ICollection<T> GetAllWhere(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));
        
        return query.Where(predicate).ToList();
    }

    public async Task<ICollection<T>> GetWithStatusAsync(StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.Where(x=>x.Status == status).ToListAsync();
    }
    
    public async Task<ICollection<T>> GetWithoutStatusAsync(StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.Where(x=>x.Status != status).ToListAsync();
    }

    public ICollection<T> GetWithStatus(StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return query.Where(x=>x.Status == status).ToList();
    }
    
    public ICollection<T> GetWithoutStatus(StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return query.Where(x=>x.Status != status).ToList();
    }

    public async Task<ICollection<T>> GetTopNByStatusAsync(int n, StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));
        
        return await query.Where(x=>x.Status == status).Take(n).ToListAsync();
    }

    public ICollection<T> GetTopNByStatus(int n, StatusEnum status, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));
        
        return query.Where(x=>x.Status == status).Take(n).ToList();
    }

    public async Task<ICollection<T>> GetByIdsAsync(IEnumerable<Guid> ids, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.Where(x=> ids.Contains(x.Id)).ToListAsync();
    }

    public ICollection<T> GetByIds(IEnumerable<Guid> ids, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return query.Where(x=> ids.Contains(x.Id)).ToList();
    }

    public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.FirstAsync(predicate);
    }

    public T GetFirst(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return query.First(predicate);
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.FirstOrDefaultAsync(predicate);
    }

    public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));
        
        return query.FirstOrDefault(predicate);
    }

    public async Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.LastAsync(predicate);
    }

    public T GetLast(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));
        
        return query.Last(predicate);
    }

    public async Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.LastOrDefaultAsync(predicate);
    }

    public T? GetLastOrDefault(Expression<Func<T, bool>> predicate, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));
        
        return query.LastOrDefault(predicate);
    }

    public async Task<T?> GetLastCreatedAsync(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync();
    }

    public T? GetLastCreated(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return query.OrderBy(x => x.CreatedAt).FirstOrDefault();
    }

    public async Task<T?> GetLastUpdatedAsync(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return await query.OrderBy(x => x.UpdatedAt).FirstOrDefaultAsync();
    }

    public T? GetLastUpdated(params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        return query.OrderBy(x => x.UpdatedAt).FirstOrDefault();
    }

    public async Task<T?> GetRandomAsync(params string[] includeProperties)
    {
        var rand = new Random();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        var entities = await query.ToListAsync();

        return entities[rand.Next(entities.Count-1)];
    }

    public T GetRandom(params string[] includeProperties)
    {
        var rand = new Random();
        
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        var entities = query.ToList();

        return entities[rand.Next(entities.Count-1)];
    }

    public async Task<T?> GetByIdAsync(Guid id, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        var entity = await query.FirstOrDefaultAsync(x=>x.Id == id);
        return entity;
    }

    public T? GetById(Guid id, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) => 
                    current.Include(includeProperty!));

        var entity = query.FirstOrDefault(x=>x.Id == id);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        await _applicationDbContext.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        _applicationDbContext.SaveChanges();
    }

    public async Task UpdateManyAsync(ICollection<T> entities)
    {
        entities.ToList().ForEach(x=>x.UpdatedAt = DateTime.UtcNow);
        
        _dbSet.UpdateRange(entities);
        await _applicationDbContext.SaveChangesAsync();
    }

    public void UpdateMany(ICollection<T> entities)
    {
        entities.ToList().ForEach(x=>x.UpdatedAt = DateTime.UtcNow);
        
        _dbSet.UpdateRange(entities);
        _applicationDbContext.SaveChanges();
    }

    public async Task RemoveSoftAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity != null)
        {
            entity.Status = StatusEnum.Deleted;

            await UpdateAsync(entity);
        }
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

    public async Task RemoveSoftAllAsync()
    {
        var entities = await GetAllAsync();

        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }

        await UpdateManyAsync(entities);
    }

    public async Task RemoveSoftWhereAsync(Expression<Func<T, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);

        foreach (var entity in entities)
        {
            entity.Status = StatusEnum.Deleted;
        }

        await UpdateManyAsync(await entities.ToListAsync());
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

    public async Task RemoveHardAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }

    public void RemoveHard(Guid id)
    {
        var entity = GetById(id);

        if (entity != null)
        {
            _dbSet.Remove(entity);
            _applicationDbContext.SaveChanges();
        }
    }

    public async Task RemoveHardAllAsync()
    {
        _dbSet.RemoveRange(await GetAllAsync());
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync();

        _dbSet.RemoveRange(entities);
        await _applicationDbContext.SaveChangesAsync();
    }

    public void RemoveHardAll()
    {
        _dbSet.RemoveRange(GetAll());
        _applicationDbContext.SaveChanges();
    }

    public void RemoveHardWhere(Expression<Func<T, bool>> predicate)
    {
        var entities = _dbSet.Where(predicate);

        _dbSet.RemoveRange(entities);
        _applicationDbContext.SaveChanges();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public bool Any(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Any(predicate);
    }

    public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AllAsync(predicate);
    }

    public bool All(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.All(predicate);
    }

    public async Task<bool> IsWithIdAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        return entity != null;
    }

    public bool IsWithId(Guid id)
    {
        return GetById(id) != null;
    }

    public async Task<bool> IsActiveAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);

        return entity?.Status == StatusEnum.Active;
    }

    public bool IsActive(Guid id)
    {
        return GetById(id)?.Status == StatusEnum.Active;
    }

    public async Task<bool> IsWithStatusAsync(StatusEnum status)
    {
        var entities = await GetAllAsync();

        return entities.Any(x => x.Status == status);
    }

    public bool IsWithStatus(StatusEnum status)
    {
        return GetAll().Any(x => x.Status == status);
    }

    public async Task<int> ExecuteSqlRawAsync(string sql, params object[] parameters)
    {
        return await _applicationDbContext.Database.ExecuteSqlRawAsync(sql, parameters);
    }

    public int ExecuteSqlRaw(string sql, params object[] parameters)
    {
        return _applicationDbContext.Database.ExecuteSqlRaw(sql, parameters);
    }
}