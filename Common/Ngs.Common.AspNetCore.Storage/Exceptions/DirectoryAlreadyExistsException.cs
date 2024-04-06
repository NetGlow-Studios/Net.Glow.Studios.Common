using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

/// <summary>
/// Exception thrown when a directory already exists.
/// </summary>
public class DirectoryAlreadyExistsException : BaseException
{
    public DirectoryAlreadyExistsException(string? message) : base(message)
    {
    }

    public DirectoryAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}