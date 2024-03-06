using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ngs.Common.AspNetCore.Infrastructure.Context;

public abstract class BaseUserIdentityDbContext<TDbContext, TUser>(DbContextOptions<TDbContext> options) 
    : IdentityUserContext<TUser, Guid>(options) where TDbContext : DbContext where TUser : IdentityUser<Guid>
{
}