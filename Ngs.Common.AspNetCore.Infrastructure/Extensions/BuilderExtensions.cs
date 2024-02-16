using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ngs.Common.AspNetCore.Infrastructure.Extensions;

public static class BuilderExtensions
{
    public static IServiceCollection AddUserIdentity<TUser, TDbContext>(this IServiceCollection services, 
        bool requireConfirmedAccount = true) where TUser : IdentityUser<Guid> where TDbContext : DbContext
    {
        services.AddDefaultIdentity<TUser>(options => options.SignIn.RequireConfirmedAccount = requireConfirmedAccount)
            .AddEntityFrameworkStores<TDbContext>();

        return services;
    }

    public static IServiceCollection AddSqlConnection<TDbContext>(this IServiceCollection services, 
        ConfigurationManager configurationManager, string migrationAssembly = "", string connectionStringKey = "DefaultConnection") where TDbContext : DbContext
    {
        if (string.IsNullOrEmpty(migrationAssembly))
        {
            migrationAssembly = typeof(TDbContext).Assembly.GetName().Name ?? throw new ArgumentNullException(nameof(migrationAssembly));
        }
        
        services.AddDbContext<TDbContext>(options 
            => options.UseSqlServer(configurationManager.GetConnectionString(connectionStringKey), x
                => x.MigrationsAssembly(migrationAssembly)));

        return services;
    }
}