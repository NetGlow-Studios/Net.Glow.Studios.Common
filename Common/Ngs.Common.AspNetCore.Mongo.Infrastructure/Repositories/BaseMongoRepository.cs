using System.Linq.Expressions;
using MongoDB.Driver;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Enums;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;
using Ngs.Common.AspNetCore.Mongo.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Mongo.Infrastructure.Repositories;

public abstract class BaseMongoRepository<T>(IMongoDatabase database, string collectionName) : IBaseRepository<T>, IBaseRepositoryReadOnly<T> where T : BaseEntity
{
    private readonly IMongoCollection<T> _collection = database.GetCollection<T>(collectionName);
    
    public T Create(T entity)
    {
        try
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Status = StatusEnum.Active;

            _collection.InsertOne(entity);

            return entity;
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedRepositoryException($"Resource ({nameof(T)}) not created", e);
        }
    }

    public IReadOnlyCollection<T> CreateMany(ICollection<T> entities)
    {
        try
        {
            foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.Status = StatusEnum.Active;
            }

            _collection.InsertMany(entities);

            return entities.ToList();
        }
        catch (Exception e)
        {
            throw new EntityNotCreatedRepositoryException($"Resources ({nameof(T)}) not created", e);
        }
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        _collection.ReplaceOne(filter, entity);
    }

    public void UpdateMany(ICollection<T> entities)
    {
        foreach (var entity in entities)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            _collection.ReplaceOne(filter, entity);
        }
    }

    public void RemoveSoft(Guid id)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        var update = Builders<T>.Update.Set(x => x.Status, StatusEnum.Deleted);
        _collection.UpdateOne(filter, update);
    }

    public void RemoveSoftAll()
    {
        var filter = Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active);
        var update = Builders<T>.Update.Set(x => x.Status, StatusEnum.Deleted);
        _collection.UpdateMany(filter, update);
    }

    public void RemoveSoftWhere(Expression<Func<T, bool>> predicate)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        var update = Builders<T>.Update.Set(x => x.Status, StatusEnum.Deleted);
        _collection.UpdateMany(filter, update);
    }

    public void RemoveHard(Guid id)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        _collection.DeleteOne(filter);
    }

    public void RemoveHardWhere(Expression<Func<T, bool>> predicate)
    {
        var filter = Builders<T>.Filter.Where(predicate);
        _collection.DeleteMany(filter);
    }

    public int Count()
    {
        return (int)_collection.CountDocuments(Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active));
    }

    public int CountWhere(Expression<Func<T, bool>> predicate)
    {
        return (int)_collection.CountDocuments(Builders<T>.Filter.Where(predicate));
    }

    public int CountByStatus(StatusEnum statusEnum)
    {
        return (int)_collection.CountDocuments(Builders<T>.Filter.Eq(x => x.Status, statusEnum));
    }

    public ICollection<T> GetAll(params string[] includeProperties)
    {
        return _collection.Find(Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active)).ToList();
    }

    public ICollection<T> GetAllWhere(Expression<Func<T, bool>> predicate)
    {
        return _collection.Find(Builders<T>.Filter.Where(predicate)).ToList();
    }

    public ICollection<T> GetWithStatus(StatusEnum status)
    {
        return _collection.Find(Builders<T>.Filter.Eq(x => x.Status, status)).ToList();
    }

    public ICollection<T> GetTopNByStatus(int n, StatusEnum status)
    {
        return _collection.Find(Builders<T>.Filter.Eq(x => x.Status, status)).Limit(n).ToList();
    }

    public ICollection<T> GetByIds(IEnumerable<Guid> ids)
    {
        return _collection.Find(Builders<T>.Filter.In(x => x.Id, ids)).ToList();
    }

    public T GetFirst(Expression<Func<T, bool>> predicate)
    {
        return _collection.Find(Builders<T>.Filter.Where(predicate)).First();
    }

    public T? GetFirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        return _collection.Find(Builders<T>.Filter.Where(predicate)).FirstOrDefault();
    }

    public T GetLast(Expression<Func<T, bool>> predicate)
    {
        return _collection.Find(Builders<T>.Filter.Where(predicate)).SortByDescending(x => x.CreatedAt).First();
    }

    public T? GetLastOrDefault(Expression<Func<T, bool>> predicate)
    {
        return _collection.Find(Builders<T>.Filter.Where(predicate)).SortByDescending(x => x.CreatedAt).FirstOrDefault();
    }

    public T? GetLastCreated()
    {
        return _collection.Find(Builders<T>.Filter.Empty).SortByDescending(x => x.CreatedAt).First();
    }

    public T? GetLastUpdated()
    {
        return _collection.Find(Builders<T>.Filter.Empty).SortByDescending(x => x.UpdatedAt).First();
    }

    public T? GetRandom()
    {
        var rand = new Random();
        var skip = rand.Next(0, Count());
        return _collection.Find(Builders<T>.Filter.Empty).Skip(skip).First();
    }

    public T? GetById(Guid id)
    {
        return _collection.Find(Builders<T>.Filter.Eq(x => x.Id, id)).SingleOrDefault();
    }

    public bool Any(Expression<Func<T, bool>> predicate)
    {
        return _collection.Find(Builders<T>.Filter.Where(predicate)).Any();
    }

    public bool All(Expression<Func<T, bool>> predicate)
    {
        return _collection.Find(Builders<T>.Filter.Where(predicate)).ToList().Count == CountWhere(predicate);
    }

    public bool IsWithId(Guid id)
    {
        return _collection.Find(Builders<T>.Filter.Eq(x => x.Id, id)).Any();
    }

    public bool IsActive(Guid id)
    {
        return _collection.Find(Builders<T>.Filter.Eq(x => x.Id, id) & Builders<T>.Filter.Eq(x => x.Status, StatusEnum.Active)).Any();
    }

    public bool IsWithStatus(StatusEnum status)
    {
        return _collection.Find(Builders<T>.Filter.Eq(x => x.Status, status)).Any();
    }
}