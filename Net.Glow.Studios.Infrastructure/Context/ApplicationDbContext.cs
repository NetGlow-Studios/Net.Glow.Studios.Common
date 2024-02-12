using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net.Glow.Studios.Domain.Entities.Users;

namespace Net.Glow.Studios.Infrastructure.DBContexts;

public class ApplicationDbContext : IdentityUserContext<UserEntity, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}