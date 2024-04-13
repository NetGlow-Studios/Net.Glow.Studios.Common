using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

public class FileAlreadyExistsException : BaseException
{
    public FileAlreadyExistsException(string? message) : base(message)
    {
    }

    public FileAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}