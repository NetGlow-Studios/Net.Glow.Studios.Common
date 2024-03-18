using Ngs.Common.AspNetCore.DataSower.Interfaces;
using Ngs.Common.AspNetCore.Entities;

namespace Ngs.Common.AspNetCore.DataSower;

/// <summary>
/// DataSeed is a class that is used to seed data into the database.
/// </summary>
/// <typeparam name="TSeed"> Seed type </typeparam>
public abstract class DataSeed<TSeed> : IDataSeed where TSeed : BaseEntity
{
    /// <summary>
    /// Seeds to be added to the database. If the seed already exists in the database, it will be ignored.
    /// </summary>
    private ICollection<TSeed> Seeds { get; } = [];

    /// <summary>
    /// Unique properties of the seed. To specify unique properties, to not override the existing data.
    /// </summary>
    public ICollection<string> UniqueProperties { get; } = [];

    /// <summary>
    /// Adds a seed to the Seeds collection
    /// </summary>
    /// <param name="seed"> Seed to be added </param>
    protected void AddSeed(TSeed seed) => Seeds.Add(seed);

    /// <summary>
    /// Adds a unique property to the UniqueProperties collection
    /// </summary>
    /// <param name="propertyName"></param>
    protected void AddUniqueProperty(string propertyName) => UniqueProperties.Add(propertyName);

    /// <summary>
    /// Seeder method that is used to seed the database. Do not call this method directly. Use the UseDataSeeds extension method instead.
    /// </summary>
    public abstract void Seeder();

    /// <summary>
    /// Returns the seeds to be added to the database.
    /// </summary>
    /// <returns> Seeds </returns>
    public ICollection<BaseEntity> GetSeeds() => Seeds.Cast<BaseEntity>().ToList();
}