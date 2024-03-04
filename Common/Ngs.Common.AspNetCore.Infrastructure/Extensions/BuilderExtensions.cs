using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Infrastructure.Repositories;
using Ngs.Common.AspNetCore.Infrastructure.Repositories.Interfaces;

namespace Ngs.Common.AspNetCore.Infrastructure.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/> to register services in the DI container.
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Adds the default identity system configuration for the specified user and database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="requireConfirmedAccount"> Whether the account confirmation is required. </param>
    /// <typeparam name="TUser"> The type of the user class. </typeparam>
    /// <typeparam name="TDbContext"> The type of the database context. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddUserIdentity<TUser, TDbContext>(this IServiceCollection services, 
        bool requireConfirmedAccount = true) where TUser : IdentityUser<Guid> where TDbContext : DbContext
    {
        services.AddDefaultIdentity<TUser>(options => options.SignIn.RequireConfirmedAccount = requireConfirmedAccount)
            .AddEntityFrameworkStores<TDbContext>();

        return services;
    }

    /// <summary>
    /// Adds the default identity system configuration for the specified user and database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="configurationManager"> The <see cref="ConfigurationManager"/> to get the connection string. </param>
    /// <param name="migrationAssembly"> The assembly name for the migrations. </param>
    /// <param name="connectionStringKey"> The key for the connection string in the configuration. </param>
    /// <typeparam name="TDbContext"> The type of the database context. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    /// <exception cref="ArgumentNullException"> The <paramref name="configurationManager"/> is null. </exception>
    public static IServiceCollection AddSqlConnection<TDbContext>(this IServiceCollection services, 
        ConfigurationManager configurationManager, string migrationAssembly = "", string connectionStringKey = "DefaultConnection") 
        where TDbContext : DbContext
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
    
    /// <summary>
    /// Adds the default identity system configuration for the specified user and database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="configurationManager"> The <see cref="ConfigurationManager"/> to get the connection string. </param>
    /// <param name="migrationAssembly"> The assembly name for the migrations. </param>
    /// <param name="connectionStringKey"> The key for the connection string in the configuration. </param>
    /// <typeparam name="TDbContext"> The type of the database context. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    /// <exception cref="ArgumentNullException"> The <paramref name="configurationManager"/> is null. </exception>
    public static IServiceCollection AddSqliteConnection<TDbContext>(this IServiceCollection services, 
        ConfigurationManager configurationManager, string migrationAssembly = "", string connectionStringKey = "DefaultConnection") 
        where TDbContext : DbContext
    {
        if (string.IsNullOrEmpty(migrationAssembly))
        {
            migrationAssembly = typeof(TDbContext).Assembly.GetName().Name ?? throw new ArgumentNullException(nameof(migrationAssembly));
        }

        services.AddDbContext<TDbContext>(options
            => options.UseSqlite(configurationManager.GetConnectionString(connectionStringKey), x
                => x.MigrationsAssembly(migrationAssembly)));

        return services;
    }

    /// <summary>
    /// Adds the default identity system configuration for the specified user and database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <typeparam name="TRepoForNamespace"> The type of the repository clas to get the assembly from. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepositories<TRepoForNamespace>(this IServiceCollection services)
    {
        return services.AddRepositories(Assembly.GetAssembly(typeof(TRepoForNamespace))!);
    }

    /// <summary>
    /// Adds the default identity system configuration for the specified user and database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="assembly"> The assembly to get the repository classes from. </param>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    /// <exception cref="ArgumentNullException"> The <paramref name="assembly"/> is null. </exception>
    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        var baseRepositoryInterface = typeof(IBaseRepositoryAsync<>);
        var baseRepositoryClass = typeof(BaseRepositoryAsync<>);

        ArgumentNullException.ThrowIfNull(assembly);

        var allTypes = assembly.GetTypes();
        
        var repositoryTypes = allTypes.Where(type 
            => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseRepositoryInterface)
               || (type.BaseType is { IsGenericType: true } && type.BaseType.GetGenericTypeDefinition() == baseRepositoryClass))
            .ToList();
        
        repositoryTypes.ForEach(x=>services.AddScoped(x));

        return services;
    }
}