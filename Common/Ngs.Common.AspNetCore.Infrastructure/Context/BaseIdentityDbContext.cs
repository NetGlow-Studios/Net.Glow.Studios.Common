using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ngs.Common.AspNetCore.Infrastructure.Context;

/// <summary>
/// Base class for IdentityDbContext
/// </summary>
/// <param name="options"> The options to be used by a DbContext. </param>
/// <typeparam name="TDbContext"> The type of context. </typeparam>
/// <typeparam name="TUser"> The type of user. </typeparam>
/// <typeparam name="TRole"> The type of role. </typeparam>
public abstract class BaseIdentityDbContext<TDbContext, TUser, TRole>(DbContextOptions<TDbContext> options) : IdentityDbContext<TUser, TRole, Guid>(options) 
    where TDbContext : DbContext where TUser : IdentityUser<Guid> where TRole : IdentityRole<Guid>
{
}