using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Ngs.Common.AspNetCore.Mongo.Infrastructure.Extensions;

public static class BuilderExtensions
{
    /// <summary>
    /// Adds MongoDB connection for the specified database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="configurationManager"> The <see cref="ConfigurationManager"/> to get the connection string. </param>
    /// <param name="connectionStringKey"></param>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddMongoConnection(this IServiceCollection services, 
        ConfigurationManager configurationManager, string connectionStringKey = "DefaultConnection") 
    {
        services.AddSingleton<IMongoClient>(new MongoClient(configurationManager.GetConnectionString(connectionStringKey)));

        return services;
    }
}