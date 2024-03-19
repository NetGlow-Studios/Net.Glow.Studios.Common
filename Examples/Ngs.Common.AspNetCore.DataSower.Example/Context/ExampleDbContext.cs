using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngs.Common.AspNetCore.DataSower.Entities;

namespace Ngs.Common.AspNetCore.DataSower.Example.Context;

//Simple DbContext for testing purposes
public class ExampleDbContext(DbContextOptions<ExampleDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SeedEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}

//Example entity configuration for testing purposes
public class SeedEntityConfiguration : IEntityTypeConfiguration<SeedEntity>
{
    public void Configure(EntityTypeBuilder<SeedEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name)
            .IsRequired();
        
        builder.Property(x => x.Key)
            .IsRequired();
        
        builder.Property(x => x.Value)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .IsRequired();
    }
}