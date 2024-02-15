using Ngs.Common.Tools.AspNetCore.Exceptions.Base;

namespace Ngs.Common.Tools.AspNetCore.Exceptions;

public class FileSizeLimitExceededException : BaseException
{
    public FileSizeLimitExceededException(string? message) : base(message)
    {
    }

    public FileSizeLimitExceededException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}