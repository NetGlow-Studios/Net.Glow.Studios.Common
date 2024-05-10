using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyBuilder = System.Reflection.Emit.PropertyBuilder;

namespace Ngs.Common.AspNetCore.Infrastructure.Extensions;

public static class EntityBuilderExtensions
{
    public static PropertyBuilder<ICollection<Guid>> HasGuidCollectionConversion(this PropertyBuilder<ICollection<Guid>> propertyBuilder)
    {
        propertyBuilder
            .HasColumnType("varchar(max)")
            .HasConversion(
                c => string.Join(",", c),
                c => c.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList())
            .Metadata.SetValueComparer(new ValueComparer<ICollection<Guid>>(
                (c1, c2) => c2 != null && c1 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));

        return propertyBuilder;
    }

    
    public static PropertyBuilder<ICollection<T>> HasCollectionConversion<T>(this PropertyBuilder<ICollection<T>> propertyBuilder)
    {
        propertyBuilder
            .HasColumnType("varchar(max)")
            .HasConversion(
                c => c.ToArray(),
                c => c.ToList())
            .Metadata.SetValueComparer(new ValueComparer<ICollection<T>>(
                (c1, c2) => c2 != null && c1 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                c => c.ToList()));

        return propertyBuilder;
    }
    
    public static PropertyBuilder<T> HasColumnDateTimeType<T>(this PropertyBuilder<T> propertyBuilder, int precision = 0)
    {
        propertyBuilder.HasColumnType($"datetime2({precision})");
            
        return propertyBuilder;
    }
    
    public static PropertyBuilder<T> HasColumnDateTimeOffsetType<T>(this PropertyBuilder<T> propertyBuilder, int precision = 0)
    {
        propertyBuilder.HasColumnType($"datetimeoffset({precision})");
            
        return propertyBuilder;
    }
}