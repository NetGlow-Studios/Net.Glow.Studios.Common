using System.Linq.Expressions;
using MongoDB.Driver;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;
using Ngs.Common.AspNetCore.Mongo.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Mongo.Infrastructure.Repositories;

public class BaseMongoRepositoryAsync<T>(IMongoDatabase database, string collectionName) : IBaseRepositoryAsync<T>, IBaseRepositoryReadOnlyAsync<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection = database.GetCollection<T>(collectionName);
    
    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return (int)await _collection.CountDocumentsAsync(Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active), cancellationToken: cancellationToken);
    }

    public async Task<int> CountWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return (int)await _collection.CountDocumentsAsync(Builders<T>.Filter.Where(predicate), cancellationToken: cancellationToken);
    }

    public async Task<int> CountByStatusAsync(StatusEnum statusEnum, CancellationToken cancellationToken = default)
    {
        return (int)await _collection.CountDocumentsAsync(Builders<T>.Filter.Eq(x => x.Status, statusEnum), cancellationToken: cancellationToken);
    }

    public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active)).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Where(predicate)).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Status, status)).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetTopNByStatusAsync(int n, StatusEnum status,
        CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Status, status)).Limit(n).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.In(x => x.Id, ids)).ToListAsync(cancellationToken);
    }

    public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Where(predicate)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Where(predicate)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T> GetLastAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Where(predicate)).SortByDescending(x => x.CreatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetLastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Where(predicate)).SortByDescending(x => x.CreatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetLastCreatedAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Empty).SortByDescending(x => x.CreatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetLastUpdatedAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Empty).SortByDescending(x => x.UpdatedAt).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> GetRandomAsync(CancellationToken cancellationToken = default)
    {
        var rand = new Random();
        var skip = rand.Next(0, await CountAsync(cancellationToken));
        return await _collection.Find(Builders<T>.Filter.Empty).Skip(skip).FirstAsync(cancellationToken: cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Id, id)).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<T>> GetPageAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active)).Skip((page - 1) * pageSize).Limit(pageSize).ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Where(predicate)).AnyAsync(cancellationToken);
    }

    public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Where(predicate)).AnyAsync(cancellationToken);
    }

    public async Task<bool> IsWithIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Id, id)).AnyAsync(cancellationToken);
    }

    public async Task<bool> IsActiveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Id, id) & Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active)).AnyAsync(cancellationToken);
    }

    public async Task<bool> IsWithStatusAsync(StatusEnum status, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<T>.Filter.Eq(x => x.Status, status)).AnyAsync(cancellationToken);
    }

    public async Task<T?> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Status = StatusEnum.Active;

            await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);

            return entity;
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedRepositoryException($"Resource ({nameof(T)}) not created", e);
        }
    }

    public async Task<ICollection<T>> CreateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            foreach (var entity in entities)
            {
                entity.Id = entity.Id == Guid.Empty ? Guid.NewGuid() : entity.Id;
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.Status = StatusEnum.Active;
            }

            await _collection.InsertManyAsync(entities, cancellationToken: cancellationToken);

            return entities.ToList();
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedRepositoryException($"Resources ({nameof(T)}) not created", e);
        }
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateManyAsync(ICollection<T> entities, CancellationToken cancellationToken = default)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
        }
    }

    public async Task RemoveSoftAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        var update = Builders<T>.Update.Set(x => x.Status, StatusEnum.Deleted);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task RemoveSoftAllAsync(CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active);
        var update = Builders<T>.Update.Set(x => x.Status, StatusEnum.Deleted);
        await _collection.UpdateManyAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task RemoveSoftWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        var update = Builders<T>.Update.Set(x => x.Status, StatusEnum.Deleted);
        await _collection.UpdateManyAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task RemoveHardAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        await _collection.DeleteOneAsync(filter, cancellationToken: cancellationToken);
    }

    public async Task RemoveHardWhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        await _collection.DeleteManyAsync(filter, cancellationToken: cancellationToken);
    }
}