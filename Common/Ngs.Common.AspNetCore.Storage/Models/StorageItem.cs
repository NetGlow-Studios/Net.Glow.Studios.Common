namespace Ngs.Common.AspNetCore.Storage.Models;

public abstract class StorageItem
{
    /// <summary>
    /// The name of this storage item.
    /// </summary>
    public string Name { get; protected set; } = string.Empty;
    
    /// <summary>
    /// The path of this storage item.
    /// </summary>
    public string Path { get; protected set; } = string.Empty;
    
    /// <summary>
    /// The full path of this storage item.
    /// </summary>
    public string FullPath { get; protected set; } = string.Empty;

    #region Checkers

    /// <summary>
    /// Check if this storage item is a directory.
    /// </summary>
    /// <returns> True if this storage item is a directory. </returns>
    public bool IsDirectory() => this is StorageDirectory || this is StorageRoot;

    /// <summary>
    /// Check if this storage item is a file.
    /// </summary>
    /// <returns> True if this storage item is a file. </returns>
    public bool IsFile() => this is StorageFile;

    /// <summary>
    /// Check if this storage item is a root.
    /// </summary>
    /// <returns> True if this storage item is a root. </returns>
    public bool IsRoot() => this is StorageRoot;

    /// <summary>
    /// Check if this storage item is a parent.
    /// </summary>
    /// <returns> True if this storage item is a parent. </returns>
    public bool IsParent() => (this as StorageDirectory)?.GetChildren().Count > 0;

    /// <summary>
    /// Check if this storage item is a child.
    /// </summary>
    /// <returns> True if this storage item is a child. </returns>
    public bool IsChild() => (this as StorageDirectory)?.Parent != null;

    /// <summary>
    /// Check if this storage item is a root child.
    /// </summary>
    /// <returns> True if this storage item is a root child. </returns>
    public bool IsRootChild() => (this as StorageDirectory)?.Parent is StorageRoot;

    /// <summary>
    /// Check if this storage item is a root parent.
    /// </summary>
    /// <returns> True if this storage item is a root parent. </returns>
    public bool IsRootParent() => (this as StorageRoot)?.GetChildren().Count > 0;

    /// <summary>
    /// Check if this storage item has a parent.
    /// </summary>
    /// <returns> True if this storage item has a parent. </returns>
    public bool HasParent() => (this as StorageDirectory)?.Parent != null || this is StorageRoot || (this as StorageFile)?.Parent != null;

    /// <summary>
    /// Check if this storage item has children.
    /// </summary>
    /// <returns> True if this storage item has children. </returns>
    public bool HasChildren() => (this as StorageDirectory)?.GetChildren().Count > 0 || (this as StorageRoot)?.GetChildren().Count > 0;
    
    /// <summary>
    /// Check if this storage item has the given child.
    /// </summary>
    /// <param name="item"> The child to check. </param>
    /// <returns> True if this storage item has the given child. </returns>
    public bool HasChild(StorageItem item) => (this as StorageDirectory)?.GetChildren().Contains(item) ?? false;
    
    /// <summary>
    /// Check if this storage item has the given child.
    /// </summary>
    /// <param name="name"> The name of the child to check. </param>
    /// <returns> True if this storage item has the given child. </returns>
    public bool HasChild(string name) => (this as StorageDirectory)?.GetChildren().Any(x => x.Name == name) ?? false;
    
    /// <summary>
    /// Check if this storage item is a child of the given directory.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool IsChildOf(StorageDirectory item) => (this as StorageDirectory)?.Parent == item || (this as StorageFile)?.Parent == item;
    
    /// <summary>
    /// Check if this storage item is a child of the given root.
    /// </summary>
    /// <param name="item"> The root to check. </param>
    /// <returns> True if this storage item is a child of the given root. </returns>
    public bool IsChildOf(StorageRoot item) => (this as StorageDirectory)?.Parent == item || (this as StorageFile)?.Parent == item;
    
    #endregion

    /// <summary>
    /// Get the parent of this storage item.
    /// </summary>
    /// <returns> The parent of this storage item. </returns>
    /// <exception cref="InvalidOperationException"> Thrown when this storage item does not have a parent. </exception>
    public StorageItem GetParent()
    {
        if(this is StorageRoot)
        {
            throw new InvalidOperationException("Root does not have a parent.");
        }
        
        if(this is StorageDirectory directory)
        {
            return directory.Parent ?? throw new InvalidOperationException("This storage item does not have a parent.");
        }
        
        return (this as StorageFile)?.Parent ?? throw new InvalidOperationException("This storage item does not have a parent.");
    }
    
    /// <summary>
    /// Try to get the parent of this storage item.
    /// </summary>
    /// <param name="parent"> The parent of this storage item. </param>
    /// <returns> True if this storage item has a parent. </returns>
    /// <exception cref="InvalidOperationException"> Thrown when this storage item does not have a parent. </exception>
    public bool TryGetParent(out StorageItem parent)
    {
        if(this is StorageRoot)
        {
            parent = default!;
            return false;
        }
        
        if(this is StorageDirectory directory)
        {
            parent = directory.Parent ?? throw new InvalidOperationException("This storage item does not have a parent.");
            return true;
        }
        
        parent = (this as StorageFile)?.Parent ?? throw new InvalidOperationException("This storage item does not have a parent.");
        return true;
    }

    /// <summary>
    /// Get the root of this storage item.
    /// </summary>
    /// <returns> The root of this storage item. </returns>
    public StorageRoot GetRoot()
    {
        var storageItem = this;

        while (true)
        {
            if(storageItem is StorageRoot rootItem)
            {
                return rootItem;
            }

            storageItem = storageItem.GetParent();
        }
    }
    
    public TStorageItem Cast<TStorageItem>() where TStorageItem : StorageItem
    {
        return (TStorageItem)this;
    }
}