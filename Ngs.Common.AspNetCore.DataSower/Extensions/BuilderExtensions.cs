using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
    /// Uses data seeds to seed the database
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
            
        foreach (var dataSeedType in dataSeedTypes)
        {
            var dataSeedInstance = (DataSeed<TSeed>)serviceProvider.GetRequiredService(dataSeedType);
            dataSeedInstance.Seeder();

            if (dataSeedInstance.UniqueProperties.Count == 0)
            {
                throw new UniquePropException("Unique properties are not defined in the seed. Define at least one unique property.");
            }
            
            var dbContext = serviceProvider.GetRequiredService<TDbContext>();
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
    
    private static IEnumerable<Type> GetDerivedTypes<TBase>()
    {
        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p 
            => typeof(TBase).IsAssignableFrom(p) && p is { IsClass: true, IsAbstract: false });
    }
}