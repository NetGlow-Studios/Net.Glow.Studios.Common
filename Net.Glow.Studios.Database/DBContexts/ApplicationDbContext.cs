using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net.Glow.Studios.Core.Entities.Users;

namespace Net.Glow.Studios.Database.DBContexts;

public class ApplicationDbContext : IdentityUserContext<AppUserEntity, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}