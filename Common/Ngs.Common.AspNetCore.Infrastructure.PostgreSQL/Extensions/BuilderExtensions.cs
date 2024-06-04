using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.PostgreSQL.Extensions;

public static class BuilderExtensions
{
    /// <summary>
    /// Adds postgres connection for the specified database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="configuration"> The <see cref="IConfigurationManager"/> to get the connection string. </param>
    /// <param name="commandTimeoutSeconds"> The command timeout in seconds. </param>
    /// <param name="migrationAssembly"> The assembly name for the migrations. </param>
    /// <param name="connectionStringKey"> The key for the connection string in the configuration. </param>
    /// <typeparam name="TDbContext"> The type of the database context. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    /// <exception cref="ArgumentNullException"> The <paramref name="configuration"/> is null. </exception>
    /// <exception cref="ConnectionNotEstablishedException"> The connection to the database could not be established. </exception>
    public static IServiceCollection AddPostgresConnection<TDbContext>(this IServiceCollection services, IConfigurationManager configuration, int commandTimeoutSeconds = 120, string migrationAssembly = "", string connectionStringKey = "DefaultConnection")
        where TDbContext : DbContext
    {
        if (string.IsNullOrEmpty(migrationAssembly))
        {
            migrationAssembly = typeof(TDbContext).Assembly.GetName().Name ??
                                throw new ArgumentNullException(nameof(migrationAssembly));
        }

        services.AddDbContext<TDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(connectionStringKey), x =>
            {
                x.MigrationsAssembly(migrationAssembly);
                x.CommandTimeout(commandTimeoutSeconds);
            });
        });

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
    /// Adds postgres connection for the specified database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="dbContextType"> The type of the database context. </param>
    /// <param name="configuration"> The <see cref="IConfiguration"/> to get the connection string. </param>
    /// <param name="migrationAssembly"> The assembly name for the migrations. </param>
    /// <param name="connectionStringKey"> The key for the connection string in the configuration. If not provided, the default is "DefaultConnection". </param>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddPostgresConnection(this IServiceCollection services, Type dbContextType, IConfigurationManager configuration, string migrationAssembly = "", string connectionStringKey = "DefaultConnection")
    {
        ArgumentNullException.ThrowIfNull(dbContextType);

        var method = typeof(BuilderExtensions).GetMethod(nameof(AddPostgresConnection),
            [typeof(IServiceCollection), typeof(ConfigurationManager), typeof(string), typeof(string)])!;
        method.MakeGenericMethod(dbContextType)
            .Invoke(null, [services, configuration, migrationAssembly, connectionStringKey]);

        return services;
    }
}