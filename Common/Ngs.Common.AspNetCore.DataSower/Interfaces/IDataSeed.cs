using Ngs.Common.AspNetCore.Entities;

namespace Ngs.Common.AspNetCore.DataSower.Interfaces;

/// <summary>
/// Interface for data seed.
/// </summary>
public interface IDataSeed
{
    /// <summary>
    /// Unique properties of the seed. To specify unique properties, to not override the existing data.
    /// </summary>
    public ICollection<string> UniqueProperties { get; }
    
    /// <summary>
    /// Seeds the data to the database. Do not call this method directly. Use UseDataSeeds extension method instead!
    /// </summary>
    public void Seeder();
    
    public ICollection<BaseEntity> GetSeeds();
}