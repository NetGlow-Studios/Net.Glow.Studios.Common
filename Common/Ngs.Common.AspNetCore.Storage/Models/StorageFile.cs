using Microsoft.AspNetCore.Http;
using Ngs.Common.AspNetCore.Storage.Compression;
using Ngs.Common.AspNetCore.Storage.Const;
using Ngs.Common.AspNetCore.Storage.Converters;
using Ngs.Common.AspNetCore.Storage.Exceptions;
using Ngs.Common.AspNetCore.Storage.Models.Files;

namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageFile : StorageItem
{
    /// <summary>
    /// File Creation Date
    /// </summary>
    public DateTime CreatedAt => GetInfo().CreationTime;

    /// <summary>
    /// File Modification Date
    /// </summary>
    public DateTime ModifiedAt => GetInfo().LastWriteTime;

    /// <summary>
    /// Parent of the file.
    /// </summary>
    public StorageFolderItem Parent { get; }

    /// <summary>
    /// File Extension
    /// </summary>
    public string Extension => GetInfo().Extension;
    
    /// <summary>
    /// File Full Name
    /// </summary>
    public string FullName => $"{Name}{Extension}";

    /// <summary>
    /// File Content Type
    /// </summary>
    public string ContentType { get; protected set; } = "application/octet-stream";

    /// <summary>
    /// File Size
    /// </summary>
    public long Size => GetInfo().Length;
    
    public StorageFileConverter Convert { get; init; }
    
    public FileCompressor Compressor { get; init; }

    public StorageFile(FileInfo file, StorageFolderItem parent)
    {
        Parent = parent;
        Name = file.Name[..^file.Extension.Length];
        AbsolutePath = file.FullName;
        RelativePath = Path.Combine(parent.RelativePath, FullName);
        Convert = new StorageFileConverter(this);
        Compressor = new FileCompressor(this);
    }

    /// <summary>
    /// Get information about the directory.
    /// </summary>
    /// <returns> Information about the directory. </returns>
    public FileInfo GetInfo() => new(AbsolutePath);


    /// <summary>
    /// Rename the file.
    /// </summary>
    /// <param name="name"> New name of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile Rename(string name)
    {
        if (name.Contains('.'))
        {
            name = name[..^Extension.Length];
        }
        
        var newAbsolutePath = AbsolutePath.Replace(Name, name);
        
        GetInfo().MoveTo(newAbsolutePath);
        
        AbsolutePath = newAbsolutePath;
        RelativePath = RelativePath.Replace(Name, name);
        Name = name;
        
        return this;
    }

    /// <summary>
    /// Remove the file.
    /// </summary>
    /// <returns> The parent folder. </returns>
    public StorageFolderItem RemoveIt()
    {
        Parent.RemoveFile(this);

        return Parent;
    }

    /// <summary>
    /// Change the extension of the file.
    /// </summary>
    /// <param name="extension"> New extension of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile ChangeExtension(string extension)
    {
        AbsolutePath = AbsolutePath.Replace(Extension, extension);
        RelativePath = RelativePath.Replace(Extension, extension);

        GetInfo().MoveTo(AbsolutePath);

        return this;
    }

    /// <summary>
    /// Move the file to another folder.
    /// </summary>
    /// <param name="directory"> New directory of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile MoveTo(StorageFolderItem directory)
    {
        return Parent.MoveFile(this, directory);
    }
    
    /// <summary>
    /// Copy the file to another folder.
    /// </summary>
    /// <param name="directory"> New directory of the file. </param>
    /// <returns> The new file. </returns>
    public StorageFile CopyTo(StorageFolderItem directory)
    {
        return Parent.CopyFile(this, directory);
    }
    
    /// <summary>
    /// Update the content of the file.
    /// </summary>
    /// <param name="content"> New content of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile UpdateContent(byte[] content)
    {
        File.WriteAllBytes(AbsolutePath, content);
        
        return this;
    }
    
    /// <summary>
    /// Update the content of the file.
    /// </summary>
    /// <param name="content"> New content of the file. </param>
    /// <returns> The file. </returns>
    public StorageFile UpdateContent(Stream content)
    {
        using var fileStream = File.Create(AbsolutePath);
        
        content.CopyTo(fileStream);
        
        return this;
    }
    
    /// <summary>
    /// Update the content of the file.
    /// </summary>
    /// <param name="content"> New content of the file. </param>
    public StorageFile UpdateContent(IFormFile content)
    {
        using var fileStream = File.Create(AbsolutePath);
        
        content.CopyTo(fileStream);
        
        return this;
    }
    
    /// <summary>
    /// Check if this storage item is a root child.
    /// </summary>
    /// <returns> True if this storage item is a root child. </returns>
    public bool IsRootChild() => Parent is StorageRoot;
    
    /// <summary>
    /// Check if this storage item is a child of the given directory.
    /// </summary>
    /// <param name="item"></param>
    /// <returns> True if this storage item is a child of the given folder. </returns>
    public bool IsChildOf(StorageFolderItem item) => Parent == item;
    
    /// <summary>
    /// Create a new instance of the file with specific type for supported extensions.
    /// </summary>
    /// <param name="file"> File to create a new instance of. </param>
    /// <param name="parent"> Parent of the file. </param>
    /// <returns> New instance of the file. </returns>
    public static StorageFile CreateNewInstance(FileInfo file, StorageFolderItem parent)
    {
        var extension = file.Extension.ToLower();

        if(SupportedFileExtensions.TextFile.Contains(extension)) 
            return new StorageTextFile(file, parent);

        if (SupportedFileExtensions.IniFile.Contains(extension)) 
            return new StorageIniFile(file, parent);
        
        if(SupportedFileExtensions.JsonFile.Contains(extension))
            return new StorageJsonFile(file, parent);
        
        if (SupportedFileExtensions.ImageFile.Contains(extension)) 
            return new StorageImageFile(file, parent);
        
        return new StorageFile(file, parent);
    }
    
    /// <summary>
    /// Cast the file to another storage file type.
    /// </summary>
    /// <typeparam name="T"> Type of the storage file. </typeparam>
    /// <returns> The file. </returns>
    public T Cast<T>() where T : StorageFile
    {
        try
        {
            return (T)this;
        }
        catch (InvalidCastException e)
        {
            throw new NotSupportedExtensionCastException("Cannot be cast to the given file type. Or the file extension is not supported. ", e);
        }
    }
}