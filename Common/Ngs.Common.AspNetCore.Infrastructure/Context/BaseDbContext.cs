using Microsoft.EntityFrameworkCore;

namespace Ngs.Common.AspNetCore.Infrastructure.Context;

public abstract class BaseDbContext<TDbContext>(DbContextOptions<TDbContext> options) : DbContext(options) where TDbContext : DbContext
{
}