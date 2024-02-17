using Ngs.Common.AspNetCore.Exceptions.Base;

namespace Ngs.Common.AspNetCore.AccessControl.Exceptions;

public class FileSizeLimitExceededException : BaseException
{
    public FileSizeLimitExceededException(string? message) : base(message)
    {
    }

    public FileSizeLimitExceededException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}