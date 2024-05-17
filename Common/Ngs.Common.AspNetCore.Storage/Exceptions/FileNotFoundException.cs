using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

public class FileNotFoundException : BaseException
{
    public FileNotFoundException(string? message) : base(message)
    {
    }

    public FileNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}