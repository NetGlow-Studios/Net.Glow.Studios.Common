using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Ngs.Common.AspNetCore.DataSower.Exceptions;
using Ngs.Common.AspNetCore.DataSower.Interfaces;
using Ngs.Common.AspNetCore.Entities;

namespace Ngs.Common.AspNetCore.DataSower.Extensions;

public static class BuilderExtensions
{
    /// <summary>
    /// Prepares seeds of specified type to be add to the database.
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <typeparam name="TDataSeed"> Data seed type </typeparam>
    /// <returns> IServiceCollection </returns>
    public static IServiceCollection AddDataSeed<TDataSeed>(this IServiceCollection services)
        where TDataSeed : IDataSeed
    {
        services.AddSingleton(typeof(TDataSeed));
        return services;
    }

    /// <summary>
    /// Adds prepared seeds to the database. Then removes the seeds from the service collection.
    /// Be careful, it's expensive! If more than one seed needs to be added use AddDataSeed method for each seed and then use UseDataSeeds method.
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <typeparam name="TDataSeed"> Data seed type </typeparam>
    /// <typeparam name="TDbContext"> Database context type </typeparam>
    /// <returns> IServiceCollection </returns>
    public static IServiceCollection AddDataSeed<TDataSeed, TDbContext>(this IServiceCollection services) where TDataSeed : IDataSeed where TDbContext : DbContext
    {
        services.AddSingleton(typeof(TDataSeed));

        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetService<TDbContext>();

        if (dbContext == null)
        {
            throw new NotExistedDbContextException($"No DbContext with type {typeof(TDbContext)} is registered. Use AddDbContext method to register the DbContext.");
        }

        var seedInstance = serviceProvider.GetRequiredService<TDataSeed>();
        seedInstance.Seeder();

        var newSeeds = seedInstance.GetSeeds();
        
        if (seedInstance.UniqueProperties.Count == 0)
        {
            throw new UniquePropException("Unique properties are not defined in the seed. Define at least one unique property.");
        }
        
        if (newSeeds.Count == 0) return services;
        
        var setMethod = dbContext.GetType()
            .GetMethods().First(method => method is { Name: "Set", IsGenericMethod: true })
            .MakeGenericMethod(seedInstance.GetType().BaseType!.GetGenericArguments()[0]).Invoke(dbContext, null)!;
        
        var existingSeeds = ((IEnumerable)setMethod).Cast<BaseEntity>().ToList();
        
        foreach (var newSeed in newSeeds)
        {
            var exists = existingSeeds.Any(existingSeed =>
            {
                foreach (var uniqueProperty in seedInstance.UniqueProperties)
                {
                    var newValue = newSeed.GetType().GetProperty(uniqueProperty)?.GetValue(newSeed);
                    var existingValue = existingSeed.GetType().GetProperty(uniqueProperty)?.GetValue(existingSeed);
        
                    if (!Equals(newValue, existingValue)) return false;
                }
        
                return true;
            });
        
            if (!exists) dbContext.Add(newSeed);
        }
        
        dbContext.SaveChanges();

        services.Remove(new ServiceDescriptor(typeof(TDataSeed), seedInstance));

        return services;
    }

    /// <summary>
    /// Adds prepared seeds to the database. Then removes the seeds from the service collection.
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <typeparam name="TDbContext"> Database context type </typeparam>
    /// <returns> IServiceCollection </returns>
    /// <exception cref="Exception"> No DbContext with type {typeof(TDbContext)} is registered. Use AddDbContext method to register the DbContext. </exception>
    /// <exception cref="UniquePropException"> Unique properties are not defined in the seed. Define at least one unique property. </exception>
    public static IServiceCollection UseDataSeeds<TDbContext>(this IServiceCollection services)
        where TDbContext : DbContext
    {
        var serviceProvider = services.BuildServiceProvider();

        var dbContext = serviceProvider.GetService<TDbContext>();

        if (dbContext == null)
        {
            throw new NotExistedDbContextException(
                $"No DbContext with type {typeof(TDbContext)} is registered. Use AddDbContext method to register the DbContext.");
        }

        // Get all data seeds from the DI container
        var dataSeeds = services.Where(s =>
        {
            if (s.ServiceType.BaseType == null) return false;
            return s.ServiceType.BaseType.IsGenericType &&
                   s.ServiceType.BaseType.GetGenericTypeDefinition() == typeof(DataSeed<>);
        }).ToList();

        // For each data seed, get the instance and call the Seeder method
        foreach (IDataSeed seedInstance in dataSeeds.Select(dataSeed =>
                     serviceProvider.GetRequiredService(dataSeed.ServiceType)))
        {
            // Call the Seeder method to prepare the seeds.
            seedInstance.Seeder();

            // Check if the unique properties are defined in the seed.
            if (seedInstance.UniqueProperties.Count == 0)
            {
                throw new UniquePropException(
                    "Unique properties are not defined in the seed. Define at least one unique property.");
            }

            // Get prepared seeds as Collection.
            var newSeeds = seedInstance.GetSeeds();

            if (newSeeds.Count == 0) continue;

            //Prepare a dbSet for the database context to get instances of the data with entity type.
            var setMethod = dbContext.GetType()
                .GetMethods().First(method => method is { Name: "Set", IsGenericMethod: true })
                .MakeGenericMethod(seedInstance.GetType().BaseType!.GetGenericArguments()[0]).Invoke(dbContext, null)!;

            // Get all existing seeds from the database.
            var existingSeeds = ((IEnumerable)setMethod).Cast<BaseEntity>().ToList();

            foreach (var newSeed in newSeeds)
            {
                // Check if the new seed exists in the database by comparing the unique properties specified in the seed.
                var exists = existingSeeds.Any(existingSeed =>
                {
                    foreach (var uniqueProperty in seedInstance.UniqueProperties)
                    {
                        var newValue = newSeed.GetType().GetProperty(uniqueProperty)?.GetValue(newSeed);
                        var existingValue = existingSeed.GetType().GetProperty(uniqueProperty)?.GetValue(existingSeed);

                        if (!Equals(newValue, existingValue)) return false;
                    }

                    return true;
                });

                // If the new seed does not exist in the database, add it to the database.
                if (!exists) dbContext.Add(newSeed);
            }
        }

        // Save the changes to the database.
        dbContext.SaveChanges();
        
        // Remove the seeds from the service collection to prevent using them again.
        dataSeeds.ForEach(dataSeed => services.Remove(dataSeed));

        return services;
    }

    /// <summary>
    /// Adds data seed to the service collection
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <param name="database"> Database name </param>
    /// <param name="collectionName"> Collection name </param>
    /// <returns> IServiceCollection </returns>
    /// <exception cref="UniquePropException"> Unique properties are not defined in the seed. Define at least one unique property. </exception>
    /// <exception cref="Exception"> No MongoClient is registered. Use AddMongoConnection method to register the MongoClient. </exception>
    public static IServiceCollection UseDataSeedsForMongo(this IServiceCollection services,
        string database, string collectionName)
    {
        var serviceProvider = services.BuildServiceProvider();
        var mongoClient = serviceProvider.GetService<IMongoClient>();

        if (mongoClient == null)
        {
            throw new Exception(
                "No MongoClient is registered. Use AddMongoConnection method to register the MongoClient.");
        }

        var databaseInstance = mongoClient.GetDatabase(database);
        var collection = databaseInstance.GetCollection<BaseEntity>(collectionName);

        var dataSeeds = services.Where(s =>
        {
            if (s.ServiceType.BaseType == null) return false;
            return s.ServiceType.BaseType.IsGenericType &&
                   s.ServiceType.BaseType.GetGenericTypeDefinition() == typeof(DataSeed<>);
        }).ToList();

        foreach (var seedInstance in dataSeeds.Select(dataSeed =>
                     serviceProvider.GetRequiredService(dataSeed.ServiceType)))
        {
            seedInstance.GetType().GetMethod(nameof(DataSeed<BaseEntity>.Seeder))?.Invoke(seedInstance, null);

            var uniqueProperties =
                seedInstance.GetType().GetProperty("UniqueProperties")?.GetValue(seedInstance)! as ICollection<string>;

            if (seedInstance.GetType().GetProperty("Seeds")?.GetValue(seedInstance)! is not ICollection newSeeds ||
                newSeeds.Count == 0) continue;

            if (uniqueProperties == null || uniqueProperties.Count == 0)
            {
                throw new UniquePropException(
                    "Unique properties are not defined in the seed. Define at least one unique property.");
            }

            var existingSeeds = collection.Find(_ => true).ToList();

            foreach (var newSeed in newSeeds)
            {
                var exists = existingSeeds.Any(existingSeed =>
                {
                    foreach (var uniqueProperty in uniqueProperties)
                    {
                        var newValue = newSeed.GetType().GetProperty(uniqueProperty)?.GetValue(newSeed);
                        var existingValue = existingSeed.GetType().GetProperty(uniqueProperty)?.GetValue(existingSeed);

                        if (!Equals(newValue, existingValue))
                        {
                            return false;
                        }
                    }

                    return true;
                });

                if (!exists)
                {
                    collection.InsertOne((newSeed as BaseEntity)!);
                }
            }
        }

        dataSeeds.ForEach(dataSeed => services.Remove(dataSeed));

        return services;
    }
}