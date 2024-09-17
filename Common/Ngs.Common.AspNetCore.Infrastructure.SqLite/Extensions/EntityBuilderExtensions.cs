using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ngs.Common.AspNetCore.Infrastructure.SqLite.Extensions;

public static class EntityBuilderExtensions
{
    public static PropertyBuilder<DateTimeOffset> HasDateTimeOffsetConversion(this PropertyBuilder<DateTimeOffset> propertyBuilder)
    {
        var converter = new ValueConverter<DateTimeOffset, string>(
            v => v.ToString("o"),
            v => DateTimeOffset.Parse(v));

        var comparer = new ValueComparer<DateTimeOffset>(
            (d1, d2) => d1.Equals(d2),
            d => d.GetHashCode(),
            d => d);

        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);

        return propertyBuilder;
    }
}