using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Tools.Exceptions;

/// <summary>
/// Exception thrown when file size limit is exceeded.
/// </summary>
public class FileSizeLimitExceededException : BaseException
{
    public FileSizeLimitExceededException(string? message) : base(message)
    {
    }

    public FileSizeLimitExceededException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}