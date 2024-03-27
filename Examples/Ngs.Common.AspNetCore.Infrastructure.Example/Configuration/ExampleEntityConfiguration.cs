using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngs.Common.AspNetCore.Infrastructure.Configuration;

namespace Ngs.Common.AspNetCore.Infrastructure.Example;

//Example of entity configuration class for ExampleEntity
public class ExampleEntityConfiguration : BaseEntityConfiguration<ExampleEntity>
{
    public override void Configure(EntityTypeBuilder<ExampleEntity> builder)
    {
        //The order of the columns should be from 1 to N in the configuration.
        //The BaseConfiguration class will order the base properties like Id (column 0), etc...
        builder.Property(x => x.Name)
            .HasColumnOrder(1)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Description)
            .HasColumnOrder(2)
            .HasMaxLength(1000)
            .IsRequired();
        
        builder.Property(x => x.Number)
            .HasColumnOrder(3)
            .IsRequired();
        
        builder.Property(x => x.Date)
            .HasColumnOrder(4)
            .HasColumnType("datetime2(0)")
            .IsRequired();
        
        base.Configure(builder);
    }
}