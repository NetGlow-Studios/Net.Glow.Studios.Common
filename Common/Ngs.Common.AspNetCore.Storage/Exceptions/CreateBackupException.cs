using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

public class CreateBackupException : BaseException
{
    public CreateBackupException()
    {
    }

    public CreateBackupException(string? message) : base(message)
    {
    }

    public CreateBackupException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}