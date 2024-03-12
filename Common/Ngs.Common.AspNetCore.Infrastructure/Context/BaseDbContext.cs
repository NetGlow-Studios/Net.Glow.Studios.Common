using Microsoft.EntityFrameworkCore;

namespace Ngs.Common.AspNetCore.Infrastructure.Context;

/// <summary>
/// Base class for all DbContexts
/// </summary>
/// <param name="options"> The options to be used by the DbContext. </param>
/// <typeparam name="TDbContext"> The type of the context. </typeparam>
public abstract class BaseDbContext<TDbContext>(DbContextOptions<TDbContext> options) : DbContext(options) where TDbContext : DbContext
{
}