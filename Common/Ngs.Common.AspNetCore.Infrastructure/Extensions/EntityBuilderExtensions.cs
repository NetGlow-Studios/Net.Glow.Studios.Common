using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngs.Common.AspNetCore.Infrastructure.Enums;

namespace Ngs.Common.AspNetCore.Infrastructure.Extensions;

public static class EntityBuilderExtensions
{
    public static PropertyBuilder<ICollection<Guid>> HasGuidCollectionConversion(this PropertyBuilder<ICollection<Guid>> propertyBuilder, SeparatorCharEnum separator = SeparatorCharEnum.Comma)
    {
        propertyBuilder
            .HasColumnType("varchar(max)")
            .HasConversion(
                v => string.Join((char)separator, v),
                v => v.Split((char)separator, StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList())
            .Metadata.SetValueComparer(new ValueComparer<ICollection<Guid>>(
                (c1, c2) => c2 != null && c1 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));

        return propertyBuilder;
    }
}