using Ngs.Common.AspNetCore.DataSower;
using Ngs.Common.AspNetCore.DataSower.Entities;

namespace Ngs.Common.AspNetCore.DataSower.Example.Seeds;

//ExampleSeeds is a class that is used to seed data into the database. Must inherit from DataSeed<T> where T is the type of the SeedEntity or a base class of the SeedEntity.
public class ExampleSeeds : DataSeed<SeedEntity>
{
    //Seeder method that is used to seed the database. Do not call this method directly! Use the UseDataSeeds extension method instead.
    public override void Seeder()
    {
        //Add unique properties to the UniqueProperties collection to specify unique properties, to not override the existing data.
        AddUniqueProperty(nameof(SeedEntity.Key));
        
        //Specify the seeds to be added to the database. If the seed already exists in the database, it will be ignored by specifying unique properties.
        AddSeed(new SeedEntity
        {
            Name = "Seed 1",
            Key = "key-for-seed-1",
            Value = "Value for seed 1",
            Description = "Description for seed 1"
        });
    }
}