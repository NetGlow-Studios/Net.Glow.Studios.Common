using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Storage.Manager;

namespace Ngs.Common.AspNetCore.Storage.Tests.Extensions;

public static class BuilderExtensions
{
    public static IServiceCollection AddLocalStorage<TStorageManager>(this IServiceCollection services, string rootPath) where TStorageManager : StorageManager
    {
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        services.AddSingleton((TStorageManager)Activator.CreateInstance(typeof(TStorageManager), rootPath)!);
        
        return services;
    }
    
    // public static IServiceCollection AddNetworkStorage(this IServiceCollection services)
    // {
    //     return services;
    // }
    //
    // public static IServiceCollection AddFtpStorage(this IServiceCollection services)
    // {
    //     return services;
    // }
    //
    // public static IServiceCollection AddSftpStorage(this IServiceCollection services)
    // {
    //     return services;
    // }
}