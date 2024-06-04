using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngs.Common.AspNetCore.Entities;
using Ngs.Common.AspNetCore.Infrastructure.Extensions;

namespace Ngs.Common.AspNetCore.Infrastructure.Configuration;

/// <summary>
/// Base class for user entity configuration
/// </summary>
/// <typeparam name="TUserEntity"></typeparam>
public class BaseUserEntityConfiguration<TUserEntity> : IEntityTypeConfiguration<TUserEntity>
    where TUserEntity : BaseUserEntity<Guid>
{
    public virtual void Configure(EntityTypeBuilder<TUserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasColumnOrder(1)
            .IsRequired();

        builder.Property(x => x.PersonalName)
            .HasMaxLength(50)
            .HasColumnOrder(2)
            .IsRequired();

        builder.Property(x => x.Surname)
            .HasMaxLength(50)
            .HasColumnOrder(3)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20)
            .HasColumnOrder(4);

        builder.Property(x => x.IsAdmin)
            .HasColumnOrder(5);

        builder.Property(x => x.EmailConfirmed)
            .HasColumnOrder(6);

        builder.Property(x => x.IsBanned)
            .HasColumnOrder(7);

        builder.Property(x => x.VerifiedAt)
            .HasColumnDateTimeOffsetType()
            .HasColumnOrder(8);

        builder.Property(x => x.LastPasswordUpdateAt)
            .HasColumnOrder(9)
            .IsRequired();

        builder.Property(x => x.LockoutEnd)
            .HasColumnOrder(10)
            .IsRequired(false);

        builder.Property(x => x.LockoutEnabled)
            .HasColumnOrder(11)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnOrder(99)
            .IsRequired();
    }
}