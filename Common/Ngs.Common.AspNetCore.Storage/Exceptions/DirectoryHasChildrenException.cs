using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

/// <summary>
/// Exception thrown when trying to delete a directory that has children.
/// </summary>
public class DirectoryHasChildrenException : BaseException
{
    public DirectoryHasChildrenException(string? message) : base(message)
    {
    }

    public DirectoryHasChildrenException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}