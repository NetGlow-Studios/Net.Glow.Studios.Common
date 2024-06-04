using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ngs.Common.AspNetCore.Infrastructure.Context;

/// <summary>
/// Base class for user identity context
/// </summary>
/// <param name="options"> The options to be used by a DbContext. </param>
/// <typeparam name="TDbContext"> The type of the context. </typeparam>
/// <typeparam name="TUser"> The type of the user. </typeparam>
public abstract class BaseUserIdentityDbContext<TDbContext, TUser>(DbContextOptions<TDbContext> options) 
    : IdentityUserContext<TUser, Guid>(options) where TDbContext : DbContext where TUser : IdentityUser<Guid>;