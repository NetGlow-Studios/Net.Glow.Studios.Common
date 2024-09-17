using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Storage.Extensions;
using Ngs.Common.AspNetCore.Storage.Models.Files;
using Ngs.Common.AspNetCore.Storage.Tests.Storage;

namespace Ngs.Common.AspNetCore.Storage.Tests;

public class New
{
    private static readonly IServiceCollection Services = new ServiceCollection();
    
    static New()
    {
        Services.AddLocalStorage<LocalContentManager>(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "StorageRoot"));
        Services.AddSmbStorage<SmbContentManager>("davex", "DsaQwe1234", "54.37.232.162");
    }
    
    [Fact]
    public static void GetStorageManager()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var storageManager = serviceProvider.GetRequiredService<LocalContentManager>();
        
        storageManager.Root.GetFile("t").Cast<StorageJsonFile>().Serialize(new {name="John", age=25});
        
        Assert.NotNull(storageManager);
    }

    [Fact]
    public void Test()
    {
        var serviceProvider = Services.BuildServiceProvider();
        var storageManager = serviceProvider.GetRequiredService<SmbContentManager>();
        
        Assert.NotNull(storageManager);
    }
}