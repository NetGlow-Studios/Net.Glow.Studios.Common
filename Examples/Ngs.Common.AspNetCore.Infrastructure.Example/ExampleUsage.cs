using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Infrastructure.Example.Repositories;
using Ngs.Common.AspNetCore.Infrastructure.Extensions;

namespace Ngs.Common.AspNetCore.Infrastructure.Example;

public class ExampleUsage
{
    public IServiceCollection Services { get; } = new ServiceCollection();
    public ConfigurationManager Configuration { get; } = new();

    public ExampleUsage()
    {
        //Add DbContext for SQL Server to DI container.
        Services.AddSqlConnection<ExampleDbContext>(Configuration, migrationAssembly: GetType().Assembly.FullName!);
        
        //Or Add DbContext for SQLite to DI container.
        Services.AddSqliteConnection<ExampleDbContext>(Configuration, migrationAssembly: GetType().Assembly.FullName!);
        
        
        //Add repositories to DI container from assembly.
        Services.AddRepositories(GetType().Assembly);
        
        //Add repositories to DI container by specified type assembly.
        Services.AddRepositories<ExampleRepository>();

        //Add repositories to DI container by specified type.
        Services.AddRepository<ExampleRepository>();
        
        //Add repositories to DI container by specified type with lifetime.
        Services.AddRepository<ExampleRepository>(ServiceLifetime.Scoped);

        //Add repositories to DI container by specified type.
        Services.AddRepository(typeof(ExampleRepository));
        
        //Add repositories to DI container by specified type with lifetime.
        Services.AddRepository(typeof(ExampleRepository), ServiceLifetime.Scoped);

        //Add repositories to DI container by specified type with factory.
        Services.AddRepository<ExampleRepository>(x => x.GetRequiredService<ExampleRepository>());
        
        //Add repositories to DI container by specified type with factory and lifetime.
        Services.AddRepository<ExampleRepository>(x => x.GetRequiredService<ExampleRepository>(), ServiceLifetime.Scoped);

        //Add repositories to DI container by specified interface and implementation.
        Services.AddRepository<IExampleRepository, ExampleRepository>();
        
        //Add repositories to DI container by specified interface and implementation with lifetime.
        Services.AddRepository<IExampleRepository, ExampleRepository>(ServiceLifetime.Scoped);
        
        //Add repositories to DI container by specified interface and implementation with factory.
        Services.AddRepository<IExampleRepository, ExampleRepository>(x => x.GetRequiredService<ExampleRepository>());
        
        //Add repositories to DI container by specified interface and implementation with factory and lifetime.
        Services.AddRepository<IExampleRepository, ExampleRepository>(x => x.GetRequiredService<ExampleRepository>(), ServiceLifetime.Scoped);
    }
}