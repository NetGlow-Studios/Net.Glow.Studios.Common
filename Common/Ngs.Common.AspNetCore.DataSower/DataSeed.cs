using Ngs.Common.AspNetCore.DataSower.Interfaces;

namespace Ngs.Common.AspNetCore.DataSower;

/// <summary>
/// DataSeed is a class that is used to seed data into the database.
/// </summary>
/// <typeparam name="TSeed"> Seed type </typeparam>
public abstract class DataSeed<TSeed> : IDataSeed where TSeed : class
{
    /// <summary>
    /// Seeds to be added to the database. If the seed already exists in the database, it will be ignored.
    /// </summary>
    public ICollection<TSeed> Seeds { get; } = new List<TSeed>();
    
    /// <summary>
    /// Unique properties that are used to check if the seed already exists in the database.
    /// </summary>
    public ICollection<string> UniqueProperties { get; } = new List<string>();
    
    /// <summary>
    /// Adds a seed to the Seeds collection
    /// </summary>
    /// <param name="seed"> Seed to be added </param>
    protected void AddSeed(TSeed seed)
    {
        Seeds.Add(seed);
    }
    
    /// <summary>
    /// Adds a unique property to the UniqueProperties collection
    /// </summary>
    /// <param name="propertyName"></param>
    protected void AddUniqueProperty(string propertyName)
    {
        UniqueProperties.Add(propertyName);
    }

    /// <summary>
    /// Seeder method that is used to seed the database. Do not call this method directly. Use the UseDataSeeds extension method instead.
    /// </summary>
    public abstract void Seeder();
}