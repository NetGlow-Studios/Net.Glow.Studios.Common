using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Storage.Manager;

namespace Ngs.Common.AspNetCore.Storage.Extensions;

/// <summary>
/// Extensions for ASP.NET Core DI container and builder.
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Add local storage manager to DI container.
    /// </summary>
    /// <param name="services"> Service collection. </param>
    /// <param name="rootPath"> Root path for storage. </param>
    /// <typeparam name="TStorageManager"> Storage manager type. </typeparam>
    /// <returns> Service collection. </returns>
    public static IServiceCollection AddLocalStorage<TStorageManager>(this IServiceCollection services, string rootPath) where TStorageManager : StorageLocalManager
    {
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
        
        services.AddSingleton((TStorageManager)Activator.CreateInstance(typeof(TStorageManager), rootPath)!);
        
        return services;
    }
}