using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

/// <summary>
/// Exception thrown when the file location is invalid.
/// </summary>
public class InvalidFileLocationException : BaseException
{
    public InvalidFileLocationException(string? message) : base(message)
    {
    }

    public InvalidFileLocationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}