using Ngs.Common.AspNetCore.Storage.Models;

namespace Ngs.Common.AspNetCore.Storage.Extensions;

public static class StorageItemExtensions
{
    #region Casters

    /// <summary>
    /// Get this storage item as a root.
    /// </summary>
    /// <returns> This storage item as a root. </returns>
    /// <exception cref="InvalidCastException"> Thrown when this storage item is not a root. </exception>
    public static StorageRoot GetAsRoot(this StorageItem item)
    {
        if (item is StorageRoot root)
        {
            return root;
        }

        throw new InvalidCastException("This storage item is not a root.");
    }

    /// <summary>
    /// Try to get this storage item as a root.
    /// </summary>
    /// <param name="item"> Storage item. </param>
    /// <param name="root"> This storage item as a root. </param>
    /// <returns> True if this storage item is a root. </returns>
    public static bool TryGetAsRoot(this StorageItem item, out StorageRoot root)
    {
        if (item is StorageRoot storageRoot)
        {
            root = storageRoot;
            return true;
        }

        root = default!;
        return false;
    }

    /// <summary>
    /// Get this storage item as a directory.
    /// </summary>
    /// <returns> This storage item as a directory. </returns>
    /// <exception cref="InvalidCastException"> Thrown when this storage item is not a directory. </exception>
    public static StorageDirectory GetAsDirectory(this StorageItem item)
    {
        if (item is StorageDirectory directory)
        {
            return directory;
        }

        throw new InvalidCastException("This storage item is not a directory.");
    }

    /// <summary>
    /// Try to get this storage item as a directory.
    /// </summary>
    /// <param name="item"> Storage item. </param>
    /// <param name="directory"> This storage item as a directory. </param>
    /// <returns> True if this storage item is a directory. </returns>
    public static bool TryGetAsDirectory(this StorageItem item, out StorageDirectory directory)
    {
        if (item is StorageDirectory storageDirectory)
        {
            directory = storageDirectory;
            return true;
        }

        directory = default!;
        return false;
    }

    /// <summary>
    /// Get this storage item as a file.
    /// </summary>
    /// <returns> This storage item as a file. </returns>
    /// <exception cref="InvalidCastException"> Thrown when this storage item is not a file. </exception>
    public static StorageFile GetAsFile(this StorageItem item)
    {
        if (item is StorageFile file)
        {
            return file;
        }

        throw new InvalidCastException("This storage item is not a file.");
    }

    /// <summary>
    /// Try to get this storage item as a file.
    /// </summary>
    /// <param name="item"> Storage item. </param>
    /// <param name="file"> This storage item as a file. </param>
    /// <returns> True if this storage item is a file. </returns>
    public static bool TryGetAsFile(this StorageItem item, out StorageFile file)
    {
        if (item is StorageFile storageFile)
        {
            file = storageFile;
            return true;
        }

        file = default!;
        return false;
    }

    #endregion
}