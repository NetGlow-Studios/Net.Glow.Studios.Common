using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

public class RestoreBackupException : BaseException
{
    public RestoreBackupException()
    {
    }

    public RestoreBackupException(string? message) : base(message)
    {
    }

    public RestoreBackupException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}