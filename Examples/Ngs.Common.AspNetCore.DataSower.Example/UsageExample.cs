using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.DataSower.Example.Context;
using Ngs.Common.AspNetCore.DataSower.Example.Seeds;
using Ngs.Common.AspNetCore.DataSower.Extensions;

namespace Ngs.Common.AspNetCore.DataSower.Example;

public class UsageExample
{
    private IServiceCollection ServiceCollection { get; }
    private IServiceProvider ServiceProvider { get; }
    
    public UsageExample()
    {
        ServiceCollection = new ServiceCollection();
        ServiceProvider = ServiceCollection.BuildServiceProvider();
        
        // Add DbContext to DI container
        ServiceCollection.AddDbContext<ExampleDbContext>();
        
        // Add DataSeed to DI container to prepare them for seeding (Seeds are not added to the database yet)
        ServiceCollection.AddDataSeed<ExampleSeeds>();
        // ServiceCollection.AddDataSeed<AnotherSeeds>();
        // ServiceCollection.AddDataSeed<YetAnotherSeeds>();
        
        // Seed the database by specifying the DbContext type
        ServiceCollection.UseDataSeeds<ExampleDbContext>();
        
        //If there is a need to seed the database with a different DbContext or just need to add single seed type
        //Use AddDbContext<TSeedData, TDbContext> overload instead of AddDataSeed<TSeedData>
        ServiceCollection.AddDataSeed<ExampleSeeds, ExampleDbContext>();
    }
}