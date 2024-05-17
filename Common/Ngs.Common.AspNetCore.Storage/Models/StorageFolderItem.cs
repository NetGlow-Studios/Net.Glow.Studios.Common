using Ngs.Common.AspNetCore.Storage.Exceptions;
using Ngs.Common.AspNetCore.Storage.Transfer;
using FileNotFoundException = Ngs.Common.AspNetCore.Storage.Exceptions.FileNotFoundException;

namespace Ngs.Common.AspNetCore.Storage.Models;

public abstract class StorageFolderItem : StorageItem
{
    private readonly List<StorageItem> _items = [];
    
    /// <summary>
    /// All items in this folder.
    /// </summary>
    public IReadOnlyList<StorageItem> Items => _items;
    
    /// <summary>
    /// All folders in this folder.
    /// </summary>
    public IReadOnlyList<StorageFolder> Folders => _items.OfType<StorageFolder>().ToList();
    
    /// <summary>
    /// All files in this folder.
    /// </summary>
    public IReadOnlyList<StorageFile> Files => _items.OfType<StorageFile>().ToList();
    
    /// <summary>
    /// Get information about the directory.
    /// </summary>
    /// <returns> Information about the directory. </returns>
    public DirectoryInfo GetInfo() => new(AbsolutePath);

    /// <summary>
    /// Rebuild the items tree in this folder.
    /// </summary>
    protected void Rebuild()
    {
        _items.Clear();
        
        var directoryInfo = new DirectoryInfo(AbsolutePath);

        foreach (var directory in directoryInfo.GetDirectories())
        {
            var directoryModel = new StorageFolder(directory, this);

            _items.Add(directoryModel);
        }

        foreach (var file in directoryInfo.GetFiles())
        {
            var fileModel = StorageFile.CreateNewInstance(file, this);

            _items.Add(fileModel);
        }
    }

    /// <summary>
    /// Get a file by name.
    /// </summary>
    /// <param name="name"> The name of the file. </param>
    /// <returns> The file with the given name. </returns>
    /// <exception cref="FileNotFoundException"> Thrown when the file is not found. </exception>
    public StorageFile GetFile(string name)
    {
       var file = Files.FirstOrDefault(x => x.Name == name);

       if (file is null)
       {
           throw new FileNotFoundException("File not found.");
       }

       return file;
    }
    
    /// <summary>
    /// Get a folder by name.
    /// </summary>
    /// <param name="name"> The name of the folder. </param>
    /// <param name="file"> The folder with the given name. </param>
    /// <returns> True if the folder is found. </returns>
    public bool TryGetFile(string name, out StorageFile file)
    {
        if(Files.Any(x => x.Name == name))
        {
            file = GetFile(name);
            
            return true;
        }
        
        file = default!;
        
        return false;
    }
    
    /// <summary>
    /// Get a folder by name.
    /// </summary>
    /// <param name="name"> The name of the folder. </param>
    /// <returns> The folder with the given name. </returns>
    public StorageFolder GetFolder(string name) => Folders.First(x => x.Name == name);
    
    /// <summary>
    /// Get a folder by name.
    /// </summary>
    /// <param name="name"> The name of the folder. </param>
    /// <param name="folder"> The folder with the given name. </param>
    /// <returns> True if the folder is found. </returns>
    public bool TryGetFolder(string name, out StorageFolder folder)
    {
        if(Folders.Any(x => x.Name == name))
        {
            folder = GetFolder(name);
            
            return true;
        }
        
        folder = default!;
        
        return false;
    }
    
    /// <summary>
    /// Add a file.
    /// </summary>
    /// <param name="fileName"> The name of the file. </param>
    /// <param name="content"> The content of the file. </param>
    /// <returns> The created file. </returns>
    public StorageFile AddFile(string fileName, byte[] content)
    {
        var path = Path.Combine(AbsolutePath, fileName);
        
        File.WriteAllBytes(path, content);
        
        var file = new StorageFile(new FileInfo(path), this);
        
        _items.Add(file);
        
        return file;
    }
    
    /// <summary>
    /// Add a file.
    /// </summary>
    /// <param name="fileName"> The name of the file. </param>
    /// <param name="content"> The content of the file. </param>
    /// <returns> The created file. </returns>
    public StorageFile AddFile(string fileName, Stream content)
    {
        var path = Path.Combine(AbsolutePath, fileName);
        
        using var fileStream = File.Create(path);
        
        content.CopyTo(fileStream);
        
        var file = new StorageFile(new FileInfo(path), this);
        
        _items.Add(file);
        
        return file;
    }
    
    /// <summary>
    /// Create a folder.
    /// </summary>
    /// <param name="name"> The name of the folder. </param>
    /// <returns> The created folder. </returns>
    public StorageFolder CreateFolder(string name)
    {
        var path = Path.Combine(AbsolutePath, name);
        
        Directory.CreateDirectory(path);
        
        var directory = new DirectoryInfo(path);
        
        var folder = new StorageFolder(directory, this);
        
        _items.Add(folder);
        
        return folder;
    }
    
    /// <summary>
    /// Remove a file by name.
    /// </summary>
    /// <param name="name"> The name of the file. </param>
    public void RemoveFile(string name) => RemoveFile(GetFile(name));
    
    /// <summary>
    /// Remove a file.
    /// </summary>
    /// <param name="file"> The file to remove. </param>
    public void RemoveFile(StorageFile file)
    {
        File.Delete(file.AbsolutePath);
        
        _items.Remove(file);
    }
    
    /// <summary>
    /// Remove a folder by name.
    /// </summary>
    /// <param name="name"> The name of the folder. </param>
    public void RemoveFolder(string name) => RemoveFolder(GetFolder(name));
    
    /// <summary>
    /// Remove a folder.
    /// </summary>
    /// <param name="folder"> The folder to remove. </param>
    public void RemoveFolder(StorageFolder folder)
    {
        Directory.Delete(folder.AbsolutePath, true);
        
        _items.Remove(folder);
    }
    
    /// <summary>
    /// Move a file to another folder.
    /// </summary>
    /// <param name="name"> The name of the file. </param>
    /// <param name="folder"> The new folder of the file. </param>
    /// <returns> The moved file. </returns>
    public StorageFile MoveFile(string name, StorageFolderItem folder) => MoveFile(GetFile(name), folder);
    
    /// <summary>
    /// Move a file to another folder.
    /// </summary>
    /// <param name="file"> The file to move. </param>
    /// <param name="folder"> The new folder of the file. </param>
    /// <returns> The moved file. </returns>
    public StorageFile MoveFile(StorageFile file, StorageFolderItem folder)
    {
        var newPath = Path.Combine(folder.AbsolutePath, file.FullName);
        
        File.Move(file.AbsolutePath, newPath);
        
        _items.Remove(file);
        
        var newFile = StorageFile.CreateNewInstance(new FileInfo(newPath), folder);
        
        folder._items.Add(newFile);
        
        return newFile;
    }
    
    /// <summary>
    /// Move a folder to another folder.
    /// </summary>
    /// <param name="name"> The name of the folder. </param>
    /// <param name="folder"> The new folder of the folder. </param>
    /// <returns> The moved folder. </returns>
    public StorageFolder MoveFolder(string name, StorageFolderItem folder) => MoveFolder(GetFolder(name), folder);
    
    /// <summary>
    /// Move a folder to another folder.
    /// </summary>
    /// <param name="folder"> The folder to move. </param>
    /// <param name="newFolder"> The new folder of the folder. </param>
    /// <returns> The moved folder. </returns>
    public StorageFolder MoveFolder(StorageFolder folder, StorageFolderItem newFolder)
    {
        var newPath = Path.Combine(newFolder.AbsolutePath, folder.Name);
        
        Directory.Move(folder.AbsolutePath, newPath);
        
        _items.Remove(folder);
        
        var newFolderModel = new StorageFolder(new DirectoryInfo(newPath), newFolder);
        
        newFolder._items.Add(newFolderModel);
        
        return newFolderModel;
    }

    /// <summary>
    /// Copy a file to another folder.
    /// </summary>
    /// <param name="name"> The name of the file. </param>
    /// <param name="directory"> The new folder of the file. </param>
    /// <returns> The copied file. </returns>
    public StorageFile CopyFile(string name, StorageFolderItem directory) => CopyFile(GetFile(name), directory);
    
    /// <summary>
    /// Copy a file to another folder.
    /// </summary>
    /// <param name="storageFile"> The file to copy. </param>
    /// <param name="directory"> The new folder of the file. </param>
    /// <returns> The copied file. </returns>
    public StorageFile CopyFile(StorageFile storageFile, StorageFolderItem directory)
    {
        var newPath = Path.Combine(directory.AbsolutePath, storageFile.FullName);
        
        File.Copy(storageFile.AbsolutePath, newPath);
        
        var newFile = StorageFile.CreateNewInstance(new FileInfo(newPath), directory);
        
        directory._items.Add(newFile);
        
        return newFile;
    }
    
    /// <summary>
    /// Copy a folder to another folder.
    /// </summary>
    /// <param name="name"> The name of the folder. </param>
    /// <param name="directory"> The new folder of the folder. </param>
    /// <returns> The copied folder. </returns>
    public StorageFolder CopyFolder(string name, StorageFolderItem directory) => CopyFolder(GetFolder(name), directory);
    
    /// <summary>
    /// Copy a folder to another folder.
    /// </summary>
    /// <param name="folder"> The folder to copy. </param>
    /// <param name="directory"> The new folder of the folder. </param>
    /// <returns> The copied folder. </returns>
    public StorageFolder CopyFolder(StorageFolder folder, StorageFolderItem directory)
    {
        var newPath = Path.Combine(directory.AbsolutePath, folder.Name);
        
        TransferManager.CopyFolder(AbsolutePath, newPath);
        
        var newFolder = new StorageFolder(new DirectoryInfo(newPath), directory);
        
        directory._items.Add(newFolder);
        
        return newFolder;
    }

    /// <summary>
    /// Find a file by relative path.
    /// </summary>
    /// <param name="relativePath"> The relative path of the file. </param>
    /// <returns> The file with the given relative path. </returns>
    /// <exception cref="FileNotFoundException"> Thrown when the file is not found. </exception>
    public StorageFile FindFile(string relativePath)
    {
        var path = relativePath;
        
        if(!RelativePath.Equals(Path.DirectorySeparatorChar.ToString()))
        {
           path = relativePath.Replace(RelativePath, string.Empty);
        }
        
        var segments = path.Split(Path.DirectorySeparatorChar);
        
        if(segments.Length == 1)
        {
            var found= Files.FirstOrDefault(x => x.FullName.Equals(segments[0], StringComparison.CurrentCultureIgnoreCase));
            
            if(found is null)
            {
                throw new FileNotFoundException("File not found.");
            }
            
            return found;
        }

        var folder = Folders.First(x => x.Name.Equals(segments[0], StringComparison.CurrentCultureIgnoreCase));
            
        return folder.FindFile(string.Join(Path.DirectorySeparatorChar, segments[1..]));
    }
    
    /// <summary>
    /// Find a folder by relative path.
    /// </summary>
    /// <param name="relativePath"> The relative path of the folder. </param>
    /// <returns> The folder with the given relative path. </returns>
    /// <exception cref="FolderNotFoundException"> Thrown when the folder is not found. </exception>
    public StorageFolder FindFolder(string relativePath)
    {
        var path = relativePath;
        
        if(!RelativePath.Equals(Path.DirectorySeparatorChar.ToString()))
        {
           path = relativePath.Replace(RelativePath, string.Empty);
        }
        
        var segments = path.Split(Path.DirectorySeparatorChar);
        
        if(segments.Length == 1)
        {
            var found= Folders.FirstOrDefault(x => x.Name.Equals(segments[0], StringComparison.CurrentCultureIgnoreCase));
            
            if(found is null)
            {
                throw new FolderNotFoundException("Folder not found.");
            }
            
            return found;
        }

        var folder = Folders.First(x => x.Name.Equals(segments[0], StringComparison.CurrentCultureIgnoreCase));
            
        return folder.FindFolder(string.Join(Path.DirectorySeparatorChar, segments[1..]));
    }
    
    /// <summary>
    /// Check if this storage item is a directory.
    /// </summary>
    /// <returns> True if this storage item is a directory. </returns>
    public bool IsFolder() => this is StorageFolder;

    /// <summary>
    /// Check if this storage item is a root.
    /// </summary>
    /// <returns> True if this storage item is a root. </returns>
    public bool IsRoot() => this is StorageRoot;
    
    /// <summary>
    /// Check if this storage item has children.
    /// </summary>
    /// <returns> True if this storage item has children. </returns>
    public bool HasItems() => Items.Count > 0;
    
    /// <summary>
    /// Check if this storage item has the given child.
    /// </summary>
    /// <param name="item"> The child to check. </param>
    /// <returns> True if this storage item has the given child. </returns>
    public bool HasItem(StorageItem item) => Items.Contains(item);
    
    /// <summary>
    /// Check if this storage item has the given child.
    /// </summary>
    /// <param name="name"> The name of the child to check. </param>
    /// <returns> True if this storage item has the given child. </returns>
    public bool HasItem(string name) => Items.Any(x => x.Name == name);
    
    /// <summary>
    /// Check if this storage item is a root child.
    /// </summary>
    /// <returns> True if this storage item is a root child. </returns>
    public bool IsRootChild() => (this as StorageFolder)?.Parent is StorageRoot;
    
    /// <summary>
    /// Check if this storage item is a child of the given directory.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool IsChildOf(StorageFolderItem item) => (this as StorageFolder)?.Parent == item;
}