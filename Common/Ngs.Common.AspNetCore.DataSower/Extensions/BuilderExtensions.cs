using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Ngs.Common.AspNetCore.DataSower.Exceptions;

namespace Ngs.Common.AspNetCore.DataSower.Extensions;

public static class BuilderExtensions
{
    /// <summary>
    /// Adds data seed to the service collection
    /// </summary>
    /// <param name="services"></param>
    /// <typeparam name="TDataSeed"> Data seed type </typeparam>
    /// <returns> IServiceCollection </returns>
    public static IServiceCollection AddDataSeed<TDataSeed> (this IServiceCollection services) where TDataSeed : class
    {
        services.AddSingleton<TDataSeed>();
        return services;
    }
    
    /// <summary>
    /// Uses data seeds to seed the database. Use this method after AddDbContext
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <typeparam name="TSeed"> Seed type </typeparam>
    /// <typeparam name="TDbContext"> Database context type </typeparam>
    /// <returns> IServiceCollection </returns>
    /// <exception cref="UniquePropException"> Unique properties are not defined in the seed. Define at least one unique property. </exception>
    public static IServiceCollection UseDataSeeds<TSeed, TDbContext>(this IServiceCollection services) where TDbContext : DbContext where TSeed : class
    {
        var serviceProvider = services.BuildServiceProvider();
        var dataSeedTypes = GetDerivedTypes<DataSeed<TSeed>>();
        
        //check if there is any DbContext with TDbContext type registered
        var dbContext = serviceProvider.GetService<TDbContext>();
        
        if (dbContext == null)
        {
            throw new Exception($"No DbContext with type {typeof(TDbContext)} is registered. Use AddDbContext method to register the DbContext.");
        }
        
        foreach (var dataSeedType in dataSeedTypes)
        {
            var dataSeedInstance = (DataSeed<TSeed>)serviceProvider.GetRequiredService(dataSeedType);
            dataSeedInstance.Seeder();

            if (dataSeedInstance.UniqueProperties.Count == 0)
            {
                throw new UniquePropException("Unique properties are not defined in the seed. Define at least one unique property.");
            }
            
            var dbSet = dbContext.Set<TSeed>();
            
            var uniqueProperties = dataSeedInstance.UniqueProperties;
            var newSeeds = dataSeedInstance.Seeds;
            var existingSeeds = dbSet.ToList();

            foreach (var newSeed in newSeeds)
            {
                var exists = false;
                
                foreach (var uniqueProperty in uniqueProperties)
                {
                    var newValue = newSeed.GetType().GetProperty(uniqueProperty)?.GetValue(newSeed, null);
                    var existingSeed = existingSeeds.FirstOrDefault(existingSeed =>
                    {
                        var existingValue = existingSeed.GetType().GetProperty(uniqueProperty)?.GetValue(existingSeed, null);
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
        }
        
        return services;
    }
    
    /// <summary>
    /// Uses data seeds to seed the MongoDB database. Use this method after AddMongoConnection
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <param name="database"> Database name </param>
    /// <param name="collectionName"> Collection name </param>
    /// <typeparam name="TSeed"> Seed type </typeparam>
    /// <returns> IServiceCollection </returns>
    /// <exception cref="UniquePropException"> Unique properties are not defined in the seed. Define at least one unique property. </exception>
    public static IServiceCollection UseDataSeedsForMongo<TSeed>(this IServiceCollection services, string database, string collectionName) where TSeed : class
    {
        var serviceProvider = services.BuildServiceProvider();
        var dataSeedTypes = GetDerivedTypes<DataSeed<TSeed>>();
            
        foreach (var dataSeedType in dataSeedTypes)
        {
            var dataSeedInstance = (DataSeed<TSeed>)serviceProvider.GetRequiredService(dataSeedType);
            dataSeedInstance.Seeder();

            if (dataSeedInstance.UniqueProperties.Count == 0)
            {
                throw new UniquePropException("Unique properties are not defined in the seed. Define at least one unique property.");
            }
            
            var mongoClient = serviceProvider.GetRequiredService<IMongoClient>();
            var databaseInstance = mongoClient.GetDatabase(database);
            var collection = databaseInstance.GetCollection<TSeed>(collectionName);
            
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
                        var existingValue = existingSeed.GetType().GetProperty(uniqueProperty)?.GetValue(existingSeed, null);
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
        }
        
        return services;
    }

    private static List<Type> GetDerivedTypes<TBase>()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var types = new List<Type>();

        foreach (var assembly in assemblies)
        {
            try
            {
                types.AddRange(assembly.GetTypes().Where(p => typeof(TBase).IsAssignableFrom(p) && p is { IsClass: true, IsAbstract: false }));
            }
            catch (System.Reflection.ReflectionTypeLoadException)
            {
                // Ignore
            }
        }
        return types;
    }
}