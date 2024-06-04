using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.PostgreSQL.Extensions;

public static class BuilderExtensions
{
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
    public static IServiceCollection AddSqliteConnection<TDbContext>(this IServiceCollection services, ConfigurationManager configurationManager, string migrationAssembly = "", string connectionStringKey = "DefaultConnection")
        where TDbContext : DbContext
    {
        if (string.IsNullOrEmpty(migrationAssembly))
        {
            migrationAssembly = typeof(TDbContext).Assembly.GetName().Name ??
                                throw new ArgumentNullException(nameof(migrationAssembly));
        }
    
        services.AddDbContext<TDbContext>(options
            => options.UseSqlite(configurationManager.GetConnectionString(connectionStringKey), x
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
    public static IServiceCollection AddSqliteConnection(this IServiceCollection services, Type dbContextType, IConfiguration configuration, string migrationAssembly = "", string connectionStringKey = "DefaultConnection")
    {
        ArgumentNullException.ThrowIfNull(dbContextType);
    
        var method = typeof(BuilderExtensions).GetMethod(nameof(AddSqliteConnection),
            [typeof(IServiceCollection), typeof(ConfigurationManager), typeof(string), typeof(string)])!;
        method.MakeGenericMethod(dbContextType)
            .Invoke(null, [services, configuration, migrationAssembly, connectionStringKey]);
    
        return services;
    }
}