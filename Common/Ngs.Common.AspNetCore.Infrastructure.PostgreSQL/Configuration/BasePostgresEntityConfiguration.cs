using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Infrastructure.Extensions;

namespace Ngs.Common.AspNetCore.Infrastructure.PostgreSQL.Configuration;

/// <summary>
/// Base entity configuration
/// </summary>
/// <typeparam name="TBase"> Base entity type </typeparam>
public abstract class BasePostgresEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
{
    /// <summary>
    /// Configure entity properties
    /// </summary>
    /// <param name="builder"> Entity type builder </param>
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnOrder(93);
        
        builder.Property(x => x.AdditionalInformation)
            .HasColumnOrder(94)
            .HasMaxLength(1000)
            .IsRequired(false);
        
        builder.Property(x => x.Tags)
            .HasColumnName("Tags")
            .HasColumnOrder(95)
            .HasMaxLength(250)
            .IsRequired(false)
            .HasConversion(
                v => string.Join(';', v), 
                v => v.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList())
            .Metadata.SetValueComparer(new ValueComparer<ICollection<string>>(
                (c1, c2) => c2 != null && c1 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));
        
        builder.Property(x => x.CreatedAt)
            .HasPostgresColumnDateTimeOffsetType()
            .HasColumnOrder(96)
            .IsRequired();

        builder.HasIndex(x => x.CreatedAt);
        
        builder.Property(x => x.CreatedBy)
            .HasColumnOrder(97)
            .IsRequired(false);
        
        builder.Property(x => x.UpdatedAt)
            .HasPostgresColumnDateTimeOffsetType()
            .HasColumnOrder(98)
            .IsRequired();
        
        builder.Property(x => x.UpdatedBy)
            .HasColumnOrder(99)
            .IsRequired(false);
    }
}