using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Infrastructure.Context;

namespace Ngs.Common.AspNetCore.Infrastructure.Example;

//Example of DbContext class for ExampleEntity
public class ExampleDbContext(DbContextOptions<ExampleDbContext> options) : BaseDbContext<ExampleDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Add the entity configuration to the model builder
        modelBuilder.ApplyConfiguration(new ExampleEntityConfiguration());
    }
}