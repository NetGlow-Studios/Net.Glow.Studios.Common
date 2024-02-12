using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Net.Glow.Studios.Core.Entities.Base;
using Net.Glow.Studios.Core.Enums.Base;
using Net.Glow.Studios.Database.DBContexts;
using Net.Glow.Studios.Database.Repositories.Base.Interfaces;

namespace Net.Glow.Studios.Database.Repositories.Base;

public class BaseRepositoryAsync<T> : IBaseRepositoryAsync<T>, IBaseRepositoryReadOnlyAsync<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;
    private readonly ApplicationDbContext _applicationDbContext;

    protected BaseRepositoryAsync(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = applicationDbContext.Set<T>();
    }

    public async Task<Guid?> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
        entity.Status = entity.Status == default ? StatusEnum.Active : entity.Status;
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        await _dbSet.AddAsync(entity, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    public async Task<ICollection<Guid>> CreateManyAsync(ICollection<T> entities,
        CancellationToken cancellationToken = default)
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

        await _dbSet.AddRangeAsync(entities, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return ids;
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }

    public async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        return entities.Count;
    }

    public async Task<int> CountByStatusAsync(StatusEnum statusEnum, CancellationToken cancellationToken = default)
    {
        return await CountWhereAsync(x => x.Status == statusEnum, cancellationToken);
    }

    public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetWithStatusAsync(StatusEnum status,
        CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(x => x.Status == status).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetWithoutStatusAsync(StatusEnum status,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(x => x.Status != status).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetTopNByStatusAsync(int n, StatusEnum status,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(x => x.Status == status).Take(n).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetByIdsAsync(IEnumerable<Guid> ids,
        CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.Where(x => ids.Contains(x.Id)).ToListAsync(cancellationToken);
    }

    public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.FirstAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T> GetLastAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.LastAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default, params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.LastOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetLastCreatedAsync(CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.OrderBy(x => x.CreatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetLastUpdatedAsync(CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.OrderBy(x => x.UpdatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetRandomAsync(CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var rand = new Random();

        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        var entities = await query.ToListAsync(cancellationToken);

        return entities[rand.Next(entities.Count - 1)];
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default,
        params string[] includeProperties)
    {
        var query = includeProperties
            .Aggregate<string?, IQueryable<T>>(
                _dbSet, (current, includeProperty) =>
                    current.Include(includeProperty!));

        return await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        _dbSet.Update(entity);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        entities.ToList().ForEach(x => x.UpdatedAt = DateTime.UtcNow);

        _dbSet.UpdateRange(entities);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
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

        await UpdateManyAsync(entities, cancellationToken);
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
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task RemoveHardAllAsync(CancellationToken cancellationToken = default)
    {
        _dbSet.RemoveRange(await GetAllAsync(cancellationToken));
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        _dbSet.RemoveRange(entities);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
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

    public async Task<int> ExecuteSqlRawAsync(string sql, CancellationToken cancellationToken = default,
        params object[] parameters)
    {
        return await _applicationDbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
    }
}