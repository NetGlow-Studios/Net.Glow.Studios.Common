using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Extensions;

public static class BuilderExtensions
{
    /// <summary>
    /// Add storage to the application
    /// </summary>
    /// <param name="services"> Service collection </param>
    /// <param name="rootPath"> Root path of the storage. This path will be used as a root for all storage operations </param>
    /// <returns> Service collection </returns>
    public static IServiceCollection AddStorage(this IServiceCollection services, string rootPath)
    {
        services.AddSingleton<IFileProvider>(new PhysicalFileProvider(rootPath));
        services.AddSingleton(new FileExtensionContentTypeProvider());
        
        var root = new StorageRoot(rootPath);
        services.AddSingleton(root);
        
        return services;
    }
}