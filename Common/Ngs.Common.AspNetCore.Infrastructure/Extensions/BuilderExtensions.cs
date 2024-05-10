using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Infrastructure.Exceptions;
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
    /// Adds repository classes from the specified assembly to the DI container.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="assembly"> The assembly to get the repository classes from. </param>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    /// <exception cref="ArgumentNullException"> The <paramref name="assembly"/> is null. </exception>
    public static IServiceCollection AddRepositories(this IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var allTypes = assembly.GetTypes();

        var repositoryTypes = allTypes.Where(type
                => type.GetInterfaces().Any(i =>
                       i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseRepositoryAsync<>))
                   || (type.BaseType is { IsGenericType: true } &&
                       type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepositoryAsync<>))
                   || type.GetInterfaces().Any(i =>
                       i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IBaseRepository<>))
                   || (type.BaseType is { IsGenericType: true } &&
                       type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>)))
            .ToList();

        repositoryTypes.ForEach(x => services.AddScoped(x));

        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <typeparam name="TRepository"> The type of the repository class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository>(this IServiceCollection services)
        where TRepository : class
    {
        services.AddScoped<TRepository>();
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <typeparam name="TRepository"> The type of the repository class. </typeparam>
    /// <typeparam name="TImplementation"> The type of the implementation class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository, TImplementation>(this IServiceCollection services)
        where TRepository : class where TImplementation : class, TRepository
    {
        services.AddScoped<TRepository, TImplementation>();
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified service lifetime.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="serviceLifetime"> The service lifetime. </param>
    /// <typeparam name="TRepository"> The type of the repository class. </typeparam>
    /// <typeparam name="TImplementation"> The type of the implementation class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository, TImplementation>(this IServiceCollection services, ServiceLifetime serviceLifetime) 
        where TRepository : class where TImplementation : class, TRepository
    {
        services.Add(new ServiceDescriptor(typeof(TRepository), typeof(TImplementation), serviceLifetime));
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified service lifetime.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="serviceLifetime"> The service lifetime. </param>
    /// <typeparam name="TRepository"> The type of the repository class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository>(this IServiceCollection services, ServiceLifetime serviceLifetime)
        where TRepository : class
    {
        services.Add(new ServiceDescriptor(typeof(TRepository), typeof(TRepository), serviceLifetime));
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified instance.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="repository"> The instance of the repository class. </param>
    /// <typeparam name="TRepository"> The type of the repository class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository>(this IServiceCollection services,
        TRepository repository)
        where TRepository : class
    {
        services.AddSingleton(repository);
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified instance and service lifetime.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="repository"> The instance of the repository class. </param>
    /// <typeparam name="TRepository"> The type of the repository interface. </typeparam>
    /// <typeparam name="TImplementation"> The type of the implementation class. </typeparam>
    /// <returns></returns>
    public static IServiceCollection AddRepository<TRepository, TImplementation>(this IServiceCollection services, TImplementation repository) 
        where TRepository : class where TImplementation : class, TRepository
    {
        services.AddSingleton<TRepository>(repository);
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified instance and service lifetime.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="repository"> The instance of the repository class. </param>
    /// <param name="serviceLifetime"> The service lifetime. </param>
    /// <typeparam name="TRepository"> The type of the repository interface. </typeparam>
    /// <typeparam name="TImplementation"> The type of the implementation class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository, TImplementation>(this IServiceCollection services, TImplementation repository, ServiceLifetime serviceLifetime) 
        where TRepository : class
        where TImplementation : class, TRepository
    {
        services.Add(new ServiceDescriptor(typeof(TRepository), repository, serviceLifetime));
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified instance and service lifetime.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="repository"> The instance of the repository class. </param>
    /// <param name="serviceLifetime"> The service lifetime. </param>
    /// <typeparam name="TRepository"> The type of the repository interface. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository>(this IServiceCollection services, TRepository repository, ServiceLifetime serviceLifetime) 
        where TRepository : class
    {
        services.Add(new ServiceDescriptor(typeof(TRepository), repository, serviceLifetime));
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified factory.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="factory"> The factory to create the repository instance. </param>
    /// <typeparam name="TRepository"> The type of the repository class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository>(this IServiceCollection services, Func<IServiceProvider, TRepository> factory)
        where TRepository : class
    {
        services.AddScoped(factory);
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified factory.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="factory"> The factory to create the repository instance. </param>
    /// <typeparam name="TRepository"> The type of the repository interface. </typeparam>
    /// <typeparam name="TImplementation"> The type of the implementation class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> factory) where TRepository : class
        where TImplementation : class, TRepository
    {
        services.AddScoped(factory);
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified factory and service lifetime.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="factory"> The factory to create the repository instance. </param>
    /// <param name="serviceLifetime"> The service lifetime. </param>
    /// <typeparam name="TRepository"> The type of the repository class. </typeparam>
    /// <typeparam name="TImplementation"> The type of the implementation class. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> factory, ServiceLifetime serviceLifetime)
        where TRepository : class where TImplementation : class, TRepository
    {
        services.Add(new ServiceDescriptor(typeof(TRepository), factory, serviceLifetime));
        return services;
    }

    /// <summary>
    /// Adds the repository class by the specified type to the DI container with the specified factory and service lifetime.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="factory"> The factory to create the repository instance. </param>
    /// <param name="serviceLifetime"> The service lifetime. </param>
    /// <typeparam name="TRepository"> The type of the repository interface. </typeparam>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddRepository<TRepository>(this IServiceCollection services, Func<IServiceProvider, TRepository> factory, ServiceLifetime serviceLifetime) 
        where TRepository : class
    {
        services.Add(new ServiceDescriptor(typeof(TRepository), factory, serviceLifetime));
        return services;
    }
}