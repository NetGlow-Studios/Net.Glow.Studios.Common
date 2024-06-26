using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;

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
    public static IServiceCollection AddUserIdentity<TUser, TDbContext>(this IServiceCollection services, bool requireConfirmedAccount = true)
        where TUser : IdentityUser<Guid> where TDbContext : DbContext
    {
        services.AddDefaultIdentity<TUser>(options => options.SignIn.RequireConfirmedAccount = requireConfirmedAccount)
            .AddEntityFrameworkStores<TDbContext>();

        return services;
    }

    /// <summary>
    /// Adds the default sql server connection for the specified database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="configurationManager"> The <see cref="ConfigurationManager"/> to get the connection string. </param>
    /// <param name="migrationAssembly"> The assembly name for the migrations. </param>
    /// <param name="connectionStringKey"> The key for the connection string in the configuration. </param>
    /// <typeparam name="TDbContext"> The type of the database context. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    /// <exception cref="ArgumentNullException"> The <paramref name="configurationManager"/> is null. </exception>
    public static IServiceCollection AddSqlConnection<TDbContext>(this IServiceCollection services, ConfigurationManager configurationManager, string migrationAssembly = "", string connectionStringKey = "DefaultConnection")
        where TDbContext : DbContext
    {
        if (string.IsNullOrEmpty(migrationAssembly))
        {
            migrationAssembly = typeof(TDbContext).Assembly.GetName().Name ??
                                throw new ArgumentNullException(nameof(migrationAssembly));
        }

        services.AddDbContext<TDbContext>(options
            => options.UseSqlServer(configurationManager.GetConnectionString(connectionStringKey), x
                => x.MigrationsAssembly(migrationAssembly)));

        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<TDbContext>();

        try
        {
            dbContext.Database.OpenConnection();
            dbContext.Database.CloseConnection();
        }
        catch (Exception e)
        {
            throw new ConnectionNotEstablishedException(string.Empty, e);
        }

        return services;
    }

    /// <summary>
    /// Adds the default sql server connection for the specified database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="dbContextType"> The type of the database context. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/> to get the connection string. </param>
    /// <param name="migrationAssembly"> The assembly name for the migrations. </param>
    /// <param name="connectionStringKey"> The key for the connection string in the configuration. If not provided, the default is "DefaultConnection". </param>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddSqlConnection(this IServiceCollection services, Type dbContextType, IConfiguration configuration, string migrationAssembly = "", string connectionStringKey = "DefaultConnection")
    {
        ArgumentNullException.ThrowIfNull(dbContextType);

        var method = typeof(BuilderExtensions).GetMethod(nameof(AddSqlConnection),
            [typeof(IServiceCollection), typeof(ConfigurationManager), typeof(string), typeof(string)])!;
        method.MakeGenericMethod(dbContextType)
            .Invoke(null, [services, configuration, migrationAssembly, connectionStringKey]);

        return services;
    }
}