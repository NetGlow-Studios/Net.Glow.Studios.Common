using Ngs.Common.AspNetCore.Storage.Enums;
using Ngs.Common.AspNetCore.Storage.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageRoot : StorageItem
{
    /// <summary>
    /// Children of this root.
    /// </summary>
    private List<StorageItem> Children { get; }
    
    public StorageRoot(string rootPath)
    {
        if (Directory.Exists(rootPath) == false)
        {
            throw new DirectoryNotFoundException($"Directory not found: {rootPath}");
        }
        
        Path = rootPath;
        Name = System.IO.Path.GetFileName(rootPath);
        Children = new List<StorageItem>();
    }
    
    /// <summary>
    /// Get the children of this root.
    /// </summary>
    /// <returns> The children of this root. </returns>
    public IReadOnlyList<StorageItem> GetChildren() => Children;
    
    /// <summary>
    /// Get the info of this root.
    /// </summary>
    /// <returns></returns>
    public DirectoryInfo GetInfo() => new(Path);
    
    /// <summary>
    /// Build the children hierarchy of this root.
    /// </summary>
    public void Build()
    {
        Children.Clear();
        
        var directoryInfo = new DirectoryInfo(Path);
        
        foreach (var directory in directoryInfo.GetDirectories())
        {
            var directoryModel = new StorageDirectory(directory, this);
            
            Children.Add(directoryModel);
        }
        
        foreach (var file in directoryInfo.GetFiles())
        {
            var fileModel = StorageFile.NewFileInstance(file, this);
            
            Children.Add(fileModel);
        }
    }
    
    /// <summary>
    /// Include a directory in the root.
    /// </summary>
    /// <param name="directory"> The directory to include. </param>
    /// <exception cref="InvalidDirectoryLocationException"> Thrown when the directory is not in the correct location. </exception>
    public void IncludeDirectory(DirectoryInfo directory)
    {
        if (directory.Parent == null || directory.Parent.FullName != Path)
        {
            throw new InvalidDirectoryLocationException($"The directory is not in the correct location. " +
                $"The current directory location is: {directory.FullName} and the parent directory location is: {Path}");
        }

        var subDirectoryModel = new StorageDirectory(directory, this);

        Children.Add(subDirectoryModel);
    }

    /// <summary>
    /// Include a file in the root.
    /// </summary>
    /// <param name="file"> The file to include. </param>
    /// <exception cref="InvalidFileLocationException"> Thrown when the file is not in the correct location. </exception>
    public void IncludeFile(FileInfo file)
    {
        if (file.Directory == null || file.Directory.FullName != Path)
        {
            throw new InvalidFileLocationException($"The file is not in the correct location. " +
                $"The current file location is: {file.FullName} and the parent directory location is: {Path}");
        }

        var fileModel = new StorageFile(file, this);

        Children.Add(fileModel);
    }
    
    /// <summary>
    /// Create a child directory in this root.
    /// </summary>
    /// <param name="name"> The name of the directory. </param>
    /// <exception cref="DirectoryAlreadyExistsException"> Thrown when the directory already exists. </exception>
    public void CreateChildDirectory(string name)
    {
        var directory = new DirectoryInfo($"{Path}{System.IO.Path.DirectorySeparatorChar}{name}");

        if (directory.Exists)
        {
            throw new DirectoryAlreadyExistsException($"The directory: {name} already exists. Cannot be created.");
        }
        
        directory.Create();
        var subDirectoryModel = new StorageDirectory(directory, this);

        Children.Add(subDirectoryModel);
    }
    
    /// <summary>
    /// Remove a child from this root.
    /// </summary>
    /// <param name="child"> The child to remove. </param>
    /// <param name="throwIfDirectory"> Throw an exception if the condition is met. </param>
    /// <exception cref="DirectoryHasChildrenException"> Thrown when the directory has children. </exception>
    /// <exception cref="ArgumentOutOfRangeException"> Thrown when the throwIfDirectory is not valid. </exception>
    /// <exception cref="DirectoryNotFoundException"> Thrown when the directory does not exist. </exception>
    public void RemoveChild(StorageDirectory child, ThrowIfDirectory throwIfDirectory = ThrowIfDirectory.HasChildren)
    {
        switch (throwIfDirectory)
        {
            case ThrowIfDirectory.HasChildren:
                if(child.HasChildren()) throw new DirectoryHasChildrenException($"The directory: {child.Name} has children. Cannot remove it.");
                break;
            case ThrowIfDirectory.Never:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(throwIfDirectory), throwIfDirectory, null);
        }
        
        var directory = new DirectoryInfo(child.Path);

        if (!directory.Exists)
        {
            throw new DirectoryNotFoundException($"The directory: {child.Name} does not exist. Cannot be removed.");
        }

        directory.Delete(true);
        Children.Remove(child);
    }
    
    /// <summary>
    /// Remove a child from this root.
    /// </summary>
    /// <param name="child"> The child to remove. </param>
    /// <exception cref="FileNotFoundException"> Thrown when the file does not exist. </exception>
    public void RemoveChild(StorageFile child)
    {
        var file = new FileInfo(child.Path);

        if (!file.Exists)
        {
            throw new FileNotFoundException($"The file: {child.Name} does not exist. Cannot be removed.");
        }

        file.Delete();
        Children.Remove(child);
    }
}