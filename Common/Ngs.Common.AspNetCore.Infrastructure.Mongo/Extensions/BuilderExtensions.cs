using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Ngs.Common.AspNetCore.Infrastructure.PostgreSQL.Extensions;

public static class BuilderExtensions
{
    /// <summary>
    /// Adds MongoDB connection for the specified database context.
    /// </summary>
    /// <param name="services"> The <see cref="IServiceCollection"/> to add the services to. </param>
    /// <param name="configurationManager"> The <see cref="IConfiguration"/> to get the connection string. </param>
    /// <param name="connectionStringKey"></param>
    /// <returns> The <see cref="IServiceCollection"/> so that additional calls can be chained. </returns>
    public static IServiceCollection AddMongoConnection(this IServiceCollection services, 
        IConfiguration configurationManager, string connectionStringKey = "DefaultConnection") 
    {
        services.AddSingleton<IMongoClient>(new MongoClient(configurationManager.GetConnectionString(connectionStringKey)));

        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new ReadOnlyCollectionSerializer<Guid>(new GuidSerializer(BsonType.String)));
        
        return services;
    }
    
    public static IServiceCollection AddMongoRepository<TRepository>(this IServiceCollection services, string databaseName, string collectionName) where TRepository : class
    {
        services.AddScoped<TRepository>(provider 
            => (TRepository)Activator.CreateInstance(typeof(TRepository), provider.GetRequiredService<IMongoClient>().GetDatabase(databaseName), collectionName)!);
        
        return services;
    }
}