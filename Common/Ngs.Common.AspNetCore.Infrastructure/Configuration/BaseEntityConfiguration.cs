using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Infrastructure.Extensions;

namespace Ngs.Common.AspNetCore.Infrastructure.Configuration;

/// <summary>
/// Base entity configuration
/// </summary>
/// <typeparam name="TBase"> Base entity type </typeparam>
public abstract class BaseEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
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
            .HasColumnOrder(94);
        
        builder.Property(x => x.Tags)
            .HasColumnOrder(95);
        
        builder.Property(x => x.CreatedAt)
            .HasColumnDateTimeOffsetType()
            .HasColumnOrder(96)
            .IsRequired();
        
        builder.Property(x => x.CreatedBy)
            .HasColumnOrder(97);
        
        builder.Property(x => x.UpdatedAt)
            .HasColumnDateTimeOffsetType()
            .HasColumnOrder(98)
            .IsRequired();
        
        builder.Property(x => x.UpdatedBy)
            .HasColumnOrder(99);
    }
}