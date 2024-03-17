using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Ngs.Common.AspNetCore.DataSower.Exceptions;
using Ngs.Common.AspNetCore.DataSower.Interfaces;

namespace Ngs.Common.AspNetCore.DataSower.Extensions;

public static class BuilderExtensions
{
    /// <summary>
    /// Adds data seed to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TDataSeed"> Data seed type </typeparam>
    /// <returns> IServiceCollection </returns>
    public static IServiceCollection AddDataSeed<TDataSeed>(this IServiceCollection services) where TDataSeed : class
    {
        services.AddSingleton<TDataSeed>();
        return services;
    }

    /// <summary>
    ///  Adds data seed to the service collection
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <typeparam name="TDataSeed"> Data seed type </typeparam>
    /// <typeparam name="TEntity"> Entity type </typeparam>
    /// <typeparam name="TDbContext"> Database context type </typeparam>
    /// <returns> IServiceCollection </returns>
    /// <exception cref="Exception"> No DbContext with type {typeof(TDbContext)} is registered. Use AddDbContext method to register the DbContext. </exception>
    /// <exception cref="UniquePropException"> Unique properties are not defined in the seed. Define at least one unique property. </exception>
    public static IServiceCollection AddDataSeed<TDataSeed, TEntity, TDbContext>(this IServiceCollection services)
        where TDataSeed : DataSeed<TEntity> where TDbContext : DbContext where TEntity : class
    {
        services.AddSingleton<TDataSeed>();
        var serviceProvider = services.BuildServiceProvider();

        var dbContext = serviceProvider.GetService<TDbContext>();

        if (dbContext == null)
        {
            throw new Exception(
                $"No DbContext with type {typeof(TDbContext)} is registered. Use AddDbContext method to register the DbContext.");
        }

        var dbSet = dbContext.Set<TEntity>();

        var seedInstance = serviceProvider.GetRequiredService<TDataSeed>();
        seedInstance.Seeder();

        var uniqueProperties =
            seedInstance.GetType().GetProperty(nameof(DataSeed<TEntity>.UniqueProperties))?.GetValue(seedInstance)! as
                ICollection<string>;
        var newSeeds =
            seedInstance.GetType().GetProperty(nameof(DataSeed<TEntity>.Seeds))?.GetValue(seedInstance)! as
                ICollection<TEntity>;

        if (newSeeds == null || newSeeds.Count == 0) return services;

        if (uniqueProperties == null || uniqueProperties.Count == 0)
        {
            throw new UniquePropException(
                "Unique properties are not defined in the seed. Define at least one unique property.");
        }

        var existingSeeds = dbSet.ToList();

        foreach (var newSeed in newSeeds)
        {
            var exists = false;

            foreach (var uniqueProperty in uniqueProperties)
            {
                var newValue = newSeed.GetType().GetProperty(uniqueProperty)?.GetValue(newSeed, null);
                var existingSeed = existingSeeds.FirstOrDefault(existingSeed =>
                {
                    var existingValue = existingSeed.GetType().GetProperty(uniqueProperty)
                        ?.GetValue(existingSeed, null);
                    return Equals(existingValue, newValue);
                });

                if (existingSeed == null) continue;

                exists = true;
                break;
            }

            if (!exists)
            {
                dbSet.Add(newSeed);
            }
        }

        dbContext.SaveChanges();

        return services;
    }

    /// <summary>
    /// Adds data seed to the service collection
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <param name="database"> Database name </param>
    /// <param name="collectionName"> Collection name </param>
    /// <typeparam name="TDataSeed"> Data seed type </typeparam>
    /// <typeparam name="TEntity"> Entity type </typeparam>
    /// <returns> IServiceCollection </returns>
    /// <exception cref="UniquePropException"> Unique properties are not defined in the seed. Define at least one unique property. </exception>
    /// <exception cref="Exception"> No MongoClient is registered. Use AddMongoConnection method to register the MongoClient. </exception>
    public static IServiceCollection UseDataSeedsForMongo<TDataSeed, TEntity>(this IServiceCollection services, string database,
        string collectionName) where TDataSeed : DataSeed<TEntity> where TEntity : class
    {
        var serviceProvider = services.BuildServiceProvider();
        var dataSeedInstance = serviceProvider.GetRequiredService<TDataSeed>();
        dataSeedInstance.Seeder();

        if (dataSeedInstance.UniqueProperties.Count == 0)
        {
            throw new UniquePropException("Unique properties are not defined in the seed. Define at least one unique property.");
        }

        var mongoClient = serviceProvider.GetService<IMongoClient>();

        if (mongoClient == null)
        {
            throw new Exception("No MongoClient is registered. Use AddMongoConnection method to register the MongoClient.");
        }

        var databaseInstance = mongoClient.GetDatabase(database);
        var collection = databaseInstance.GetCollection<TEntity>(collectionName);

        var uniqueProperties = dataSeedInstance.UniqueProperties;
        var newSeeds = dataSeedInstance.Seeds;
        var existingSeeds = collection.Find(_ => true).ToList();

        foreach (var newSeed in newSeeds)
        {
            var exists = false;

            foreach (var uniqueProperty in uniqueProperties)
            {
                var newValue = newSeed.GetType().GetProperty(uniqueProperty)?.GetValue(newSeed, null);
                var existingSeed = existingSeeds.FirstOrDefault(existingSeed =>
                {
                    var existingValue = existingSeed.GetType().GetProperty(uniqueProperty)
                        ?.GetValue(existingSeed, null);
                    return Equals(existingValue, newValue);
                });

                if (existingSeed == null) continue;

                exists = true;
                break;
            }

            if (!exists)
            {
                collection.InsertOne(newSeed);
            }
        }

        return services;
    }
}