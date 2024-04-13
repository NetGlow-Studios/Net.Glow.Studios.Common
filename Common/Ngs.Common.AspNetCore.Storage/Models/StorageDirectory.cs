using Ngs.Common.AspNetCore.Storage.Compression;
using Ngs.Common.AspNetCore.Storage.Enums;
using Ngs.Common.AspNetCore.Storage.Exceptions;
using Ngs.Common.AspNetCore.Storage.Extensions;
using Ngs.Common.AspNetCore.Storage.Transfer;

namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageDirectory : StorageItem
{
    /// <summary>
    /// Children of the directory.
    /// </summary>
    private List<StorageItem> Children { get; }

    /// <summary>
    /// Parent of the directory.
    /// </summary>
    public StorageItem Parent { get; private set; }

    /// <summary>
    /// Compressor instance for the directory.
    /// </summary>
    public DirectoryCompressor Compressor => new(this);

    public StorageDirectory(DirectoryInfo directory, StorageItem parent)
    {
        Name = directory.Name;
        Path = string.Concat(parent.Path, directory.Name, System.IO.Path.DirectorySeparatorChar);
        FullPath = directory.FullName;
        Parent = parent;
        Children = new List<StorageItem>();

        foreach (var subDirectory in directory.GetDirectories())
        {
            var subDirectoryModel = new StorageDirectory(subDirectory, this);

            Children.Add(subDirectoryModel);
        }

        foreach (var file in directory.GetFiles())
        {
            var fileModel = StorageFile.NewFileInstance(file, parent);

            Children.Add(fileModel);
        }
    }

    /// <summary>
    /// Get the children of the directory.
    /// </summary>
    /// <returns> Children of the directory. </returns>
    public IReadOnlyList<StorageItem> GetChildren() => Children;

    /// <summary>
    /// Get information about the directory.
    /// </summary>
    /// <returns> Information about the directory. </returns>
    public DirectoryInfo GetInfo() => new(Path);

    /// <summary>
    /// Include a directory to the directory.
    /// </summary>
    /// <param name="directory"> Directory to include. </param>
    /// <returns> The directory. </returns>
    /// <exception cref="InvalidDirectoryLocationException"> Thrown when the directory is not in the correct location. </exception>
    public StorageDirectory IncludeDirectory(DirectoryInfo directory)
    {
        if (directory.Parent is null || directory.Parent.FullName != FullPath)
        {
            throw new InvalidDirectoryLocationException($"The directory is not in the correct location. " +
                $"The current directory location is: {directory.FullName} and the parent directory location is: {Path}");
        }

        var subDirectoryModel = new StorageDirectory(directory, this);

        Children.Add(subDirectoryModel);

        return this;
    }

    /// <summary>
    /// Include a file to the directory.
    /// </summary>
    /// <param name="file"> File to include. </param>
    /// <returns> The file. </returns>
    /// <exception cref="InvalidFileLocationException"> Thrown when the file is not in the correct location. </exception>
    public StorageFile IncludeFile(FileInfo file)
    {
        if (file.Directory is null || file.Directory.FullName.TrimEnd('\\') != FullPath.TrimEnd('\\'))
        {
            throw new InvalidFileLocationException($"The file is not in the correct location. " +
                $"The current file location is: {file.FullName} and the parent directory location is: {Path}");
        }

        var fileModel = new StorageFile(file, this);

        Children.Add(fileModel);

        return fileModel;
    }
    
    public StorageFile IncludeFile(string fileName, Stream stream)
    {
        var file = new FileInfo($"{FullPath}{System.IO.Path.DirectorySeparatorChar}{fileName}");

        if (file.Exists)
        {
            throw new FileAlreadyExistsException($"The file: {fileName} already exists.");
        }
        
        using var fileStream = file.Create();
        stream.CopyTo(fileStream);

        var fileModel = new StorageFile(file, this);

        Children.Add(fileModel);

        return fileModel;
    }

    /// <summary>
    /// Create a new directory in the current directory.
    /// </summary>
    /// <param name="name" > Name of the new directory. </param>
    /// <param name="throwIfDirectoryExists"> Throw an exception if the directory already exists. </param>
    /// <returns> The created directory. </returns>
    /// <exception cref="DirectoryAlreadyExistsException"></exception>
    public StorageDirectory CreateChildDirectory(string name, bool throwIfDirectoryExists = true)
    {
        var directory = new DirectoryInfo($"{FullPath}{System.IO.Path.DirectorySeparatorChar}{name}");

        if (directory.Exists)
        {
            if (throwIfDirectoryExists)
            {
                throw new DirectoryAlreadyExistsException($"The directory: {name} already exists. Cannot be created.");
            }

            return Children.First(x=>x.Name == name).Cast<StorageDirectory>();
        }

        directory.Create();
        var subDirectoryModel = new StorageDirectory(directory, this);

        Children.Add(subDirectoryModel);

        return subDirectoryModel;
    }

    /// <summary>
    /// Remove a directory from the current directory.
    /// </summary>
    /// <param name="directory"> Directory to remove. </param>
    /// <param name="throwIfDirectory"> Throw an exception if the condition is met. </param>
    /// <returns> The current directory. </returns>
    /// <exception cref="DirectoryHasChildrenException"> Thrown when the directory has children. </exception>
    /// <exception cref="ArgumentOutOfRangeException"> Thrown when the condition is not properly set. </exception>
    /// <exception cref="DirectoryNotFoundException"> Thrown when the directory does not exist. </exception>
    public StorageDirectory RemoveChild(StorageDirectory directory,
        ThrowIfDirectory throwIfDirectory = ThrowIfDirectory.HasChildren)
    {
        switch (throwIfDirectory)
        {
            case ThrowIfDirectory.HasChildren:
                if (directory.HasChildren())
                    throw new DirectoryHasChildrenException(
                        $"The directory: {directory.Name} has children. Cannot remove it.");
                break;
            case ThrowIfDirectory.Never:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(throwIfDirectory), throwIfDirectory, null);
        }

        var directoryInfo = new DirectoryInfo(directory.FullPath);

        if (!directoryInfo.Exists)
        {
            throw new DirectoryNotFoundException($"The directory: {directory.Name} does not exist. Cannot be removed.");
        }

        directoryInfo.Delete(true);
        Children.Remove(directory);

        return this;
    }

    /// <summary>
    /// Remove a file from the current directory.
    /// </summary>
    /// <param name="child"> File to remove. </param>
    /// <returns> The current directory. </returns>
    /// <exception cref="FileNotFoundException"> Thrown when the file does not exist. </exception>
    public StorageDirectory RemoveChild(StorageFile child)
    {
        var file = new FileInfo(child.FullPath);

        if (!file.Exists)
        {
            throw new FileNotFoundException($"The file: {child.Name} does not exist. Cannot be removed.");
        }

        file.Delete();
        Children.Remove(child);

        return this;
    }

    /// <summary>
    /// Remove a child from the current directory.
    /// </summary>
    /// <param name="index"> Index of the child to remove. </param>
    /// <returns> The current directory. </returns>
    /// <exception cref="ArgumentOutOfRangeException"> Thrown when the index is out of range. </exception>
    public StorageDirectory RemoveChildAt(int index)
    {
        if (index < 0 || index >= Children.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), index, "The index is out of range.");
        }

        var child = Children[index];

        switch (child)
        {
            case StorageDirectory directory:
                RemoveChild(directory);
                break;
            case StorageFile file:
                RemoveChild(file);
                break;
        }

        return this;
    }

    /// <summary>
    /// Rename the current directory.
    /// </summary>
    /// <param name="name"> New name of the directory. </param>
    /// <returns> The current directory. </returns>
    public StorageDirectory Rename(string name)
    {
        var directory = new DirectoryInfo(Path);
        var newPath = Path.Replace(Name, name);

        directory.MoveTo(newPath);

        Path = Path.Replace(Name, name);
        Name = name;

        return this;
    }

    public StorageDirectory GetChildDirectory(string name)
    {
        var child = Children.FirstOrDefault(x => x.Name == name);

        if (child is null)
        {
            throw new DirectoryNotFoundException($"The directory: {name} does not exist.");
        }

        if (child is not StorageDirectory directory)
        {
            throw new InvalidCastException($"The child: {name} is not a directory.");
        }

        return directory;
    }

    /// <summary>
    /// Move a folder into the current directory.
    /// </summary>
    /// <param name="folderToMove"> Folder to move. </param>
    /// <returns> The moved folder. </returns>
    /// <exception cref="DirectoryNotFoundException"> Thrown when the directory does not exist. </exception>
    public StorageDirectory MoveIntoFrom(StorageDirectory folderToMove)
    {
        var directory = new DirectoryInfo(folderToMove.Path);

        if (!directory.Exists)
        {
            throw new DirectoryNotFoundException($"The directory: {folderToMove.Name} does not exist.");
        }

        var newDirectory = new DirectoryInfo($"{Path}{System.IO.Path.DirectorySeparatorChar}{folderToMove.Name}");

        if (newDirectory.Exists)
        {
            throw new DirectoryAlreadyExistsException($"The directory: {folderToMove.Name} already exists.");
        }

        directory.MoveTo(newDirectory.FullName);
        Children.Add(folderToMove);

        return folderToMove;
    }

    public StorageDirectory CopyIntoFrom(StorageDirectory folderToCopy)
    {
        var directory = new DirectoryInfo(folderToCopy.FullPath);

        if (!directory.Exists)
        {
            throw new DirectoryNotFoundException($"The directory: {folderToCopy.Name} does not exist.");
        }

        var newDirectory = new DirectoryInfo($"{FullPath}{System.IO.Path.DirectorySeparatorChar}{folderToCopy.Name}");

        if (newDirectory.Exists)
        {
            throw new DirectoryAlreadyExistsException($"The directory: {folderToCopy.Name} already exists.");
        }

        TransferManager.CopyFolder(folderToCopy.FullPath, newDirectory.FullName);

        var newFolder = new StorageDirectory(newDirectory, this);
        Children.Add(newFolder);

        return newFolder;
    }
    
    public StorageDirectory CopyContentIntoFrom(StorageDirectory folderToCopy)
    {
        var directory = new DirectoryInfo(folderToCopy.FullPath);

        if (!directory.Exists)
        {
            throw new DirectoryNotFoundException($"The directory: {folderToCopy.Name} does not exist.");
        }
        
        foreach (var subDirectory in directory.GetDirectories())
        {
            var newDirectory = new DirectoryInfo($"{FullPath}{System.IO.Path.DirectorySeparatorChar}{subDirectory.Name}");

            if (newDirectory.Exists)
            {
                throw new DirectoryAlreadyExistsException($"The directory: {subDirectory.Name} already exists.");
            }

            TransferManager.CopyFolder(subDirectory.FullName, newDirectory.FullName);

            var newFolder = new StorageDirectory(newDirectory, this);
            Children.Add(newFolder);
        }
        
        foreach (var file in directory.GetFiles())
        {
            var newFile = new FileInfo($"{FullPath}{System.IO.Path.DirectorySeparatorChar}{file.Name}");

            if (newFile.Exists)
            {
                throw new FileAlreadyExistsException($"The file: {file.Name} already exists.");
            }

            TransferManager.CopyFile(file.FullName, newFile.FullName);

            var newFileModel = new StorageFile(newFile, this);
            Children.Add(newFileModel);
        }

        return this;
    }

    /// <summary>
    /// Remove the current directory.
    /// </summary>
    /// <returns> The parent directory. </returns>
    /// <exception cref="NotImplementedException"> Thrown when the directory is the root directory. </exception>
    public StorageDirectory RemoveIt(ThrowIfDirectory throwIf = ThrowIfDirectory.HasChildren)
    {
        var parent = Parent.GetAsDirectory();
        
        parent.RemoveChild(this.Cast<StorageDirectory>(), throwIf);

        return parent;
    }

    public StorageDirectory GetFromPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) return this;
        
        var pathSegments = path.Split(System.IO.Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);
        
        if (pathSegments.Length == 1)
        {
            return Children.First(x => x.IsDirectory() && x.Name.Equals(pathSegments[0]
                .Replace("/", "").Replace("\\",""), StringComparison.CurrentCultureIgnoreCase)).GetAsDirectory();
        }
        
        foreach (var child in Children)
        {
            if (child.IsDirectory() && child.Name.Equals(pathSegments[0].Replace("/", ""), StringComparison.CurrentCultureIgnoreCase))
            {
                return child.GetAsDirectory().GetFromPath(string.Join(System.IO.Path.DirectorySeparatorChar, pathSegments[1..]));
            }
        }
        
        throw new DirectoryNotFoundException("Directory not found");
    }
}