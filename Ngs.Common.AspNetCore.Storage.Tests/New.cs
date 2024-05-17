using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Storage.Tests.Extensions;
using Ngs.Common.AspNetCore.Storage.Tests.Storage;

namespace Ngs.Common.AspNetCore.Storage.Tests;

public class New
{
    private static readonly IServiceCollection Services = new ServiceCollection();
    
    static New()
    {
        Services.AddLocalStorage<LocalContentManager>(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StorageRoot"));
    }
    
    [Fact]
    public static void GetStorageManager()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var storageManager = serviceProvider.GetRequiredService<LocalContentManager>();
        
        Assert.NotNull(storageManager);
    }
}