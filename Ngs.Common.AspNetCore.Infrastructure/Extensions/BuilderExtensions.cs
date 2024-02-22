using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Base;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Base.Interfaces;

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
        ConfigurationManager configurationManager, string migrationAssembly = "",
        string connectionStringKey = "DefaultConnection") where TDbContext : DbContext
    {
        if (string.IsNullOrEmpty(migrationAssembly))
        {
            migrationAssembly = typeof(TDbContext).Assembly.GetName().Name ??
                                throw new ArgumentNullException(nameof(migrationAssembly));
        }

        services.AddDbContext<TDbContext>(options
            => options.UseSqlServer(configurationManager.GetConnectionString(connectionStringKey), x
                => x.MigrationsAssembly(migrationAssembly)));

        return services;
    }

    public static IServiceCollection AddRepositories<TRepoForNamespace>(this IServiceCollection services)
    {
        return services.AddRepositories(Assembly.GetAssembly(typeof(TRepoForNamespace))!);
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        var baseRepositoryInterface = typeof(IBaseRepositoryAsync<>);
        var baseRepositoryClass = typeof(BaseRepositoryAsync<>);

        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }
        
        var allTypes = assembly.GetTypes();
        
        var repositoryTypes = allTypes.Where(type 
            => (type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseRepositoryInterface))
               || (type.BaseType is { IsGenericType: true } && type.BaseType.GetGenericTypeDefinition() == baseRepositoryClass)
        ).ToList();
        
        foreach (var repositoryType in repositoryTypes)
        {
            services.AddScoped(repositoryType);
        }

        return services;
    }
}