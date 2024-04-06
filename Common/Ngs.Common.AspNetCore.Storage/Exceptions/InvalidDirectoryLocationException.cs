using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

/// <summary>
/// Thrown when the directory location is invalid.
/// </summary>
public class InvalidDirectoryLocationException : BaseException
{
    public InvalidDirectoryLocationException(string? message) : base(message)
    {
    }

    public InvalidDirectoryLocationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}