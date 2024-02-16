using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ngs.Common.AspNetCore.Infrastructure.Context.Base;

public class BaseUserIdentityDbContext<TDbContext, TUser>(DbContextOptions<TDbContext> options) 
    : IdentityUserContext<TUser, Guid>(options) where TDbContext : DbContext where TUser : IdentityUser<Guid>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}