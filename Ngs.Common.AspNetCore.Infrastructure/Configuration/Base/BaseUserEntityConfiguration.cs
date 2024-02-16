using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ngs.Common.AspNetCore.Entities.Base;

namespace Ngs.Common.AspNetCore.Infrastructure.Configuration.Base;

public class BaseUserEntityConfiguration<TUserEntity> : IEntityTypeConfiguration<TUserEntity> where TUserEntity : BaseUserEntity<Guid>
{
    public virtual void Configure(EntityTypeBuilder<TUserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnOrder(0)
            .IsRequired();
        
        builder.Property(x => x.PersonalName)
            .HasMaxLength(50)
            .HasColumnOrder(1)
            .IsRequired();
        
        builder.Property(x => x.Surname)
            .HasMaxLength(50)
            .HasColumnOrder(2)
            .IsRequired();

        builder.Property(x => x.UserName)
            .HasColumnOrder(3);
        
        builder.Property(x => x.IsAdmin)
            .HasColumnOrder(4);
        
        builder.Property(x => x.EmailConfirmed)
            .HasColumnOrder(5);
        
        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime2(0)")
            .HasColumnOrder(99)
            .IsRequired();
        
        builder.Property(x => x.VerifiedAt)
            .HasColumnType("datetime2(0)")
            .HasColumnOrder(6);
        
        builder.Property(x => x.LastPasswordUpdateAt)
            .HasColumnType("datetime2(0)")
            .HasColumnOrder(7)
            .IsRequired();
    }
}