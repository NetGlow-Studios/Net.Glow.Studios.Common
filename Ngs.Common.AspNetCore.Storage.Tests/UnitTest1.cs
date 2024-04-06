using Ngs.Common.AspNetCore.Storage.Extensions;
using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Tests;

public class UnitTest1
{
    private static readonly StorageRoot Root = new(@"C:\Users\davex\OneDrive\Desktop\StorageRoot");

    static UnitTest1()
    {
        Root.Build();
    }

    [Fact]
    public void Storage_Root_Should_Not_Be_Null()
    {
        Assert.NotNull(Root);
    }
    
    [Fact]
    public void Storage_Root_Create_Children()
    {
        Root.CreateChildDirectory("TestDirectory");

        (Root.GetChildren().First(x => x.IsDirectory()) as StorageFile)!.Compressor.Zip();
        
        Assert.NotEmpty(Root.GetChildren());
    }

    [Fact]
    public void Storage_Root_Remove_Children()
    {
        var child = Root.GetChildren()[0];
        Root.GetAsDirectory().RemoveChild(child.GetAsDirectory());
    }
    
    [Fact]
    public void Storage_File_Rename()
    {
        var file = Root.GetChildren().First(x => x.IsFile() && x.Name.Equals("a.txt", StringComparison.CurrentCultureIgnoreCase)).GetAsFile();
        var filePath = file.Path;
        file.Rename("TestFile.txt");
        
        Assert.Equal("TestFile.txt", file.Name);
        Assert.Equal(filePath.Replace("a.txt", "TestFile.txt"), file.Path);
        
        file.Rename("a.txt");
    }

    [Fact]
    public void T()
    {
        var file = Root.GetChildren().First(x => x.Name.Equals("futuristic city 0.png", StringComparison.CurrentCultureIgnoreCase)).Cast<StorageImageFile>();

        file.Converter.ToSvg();
    }
}