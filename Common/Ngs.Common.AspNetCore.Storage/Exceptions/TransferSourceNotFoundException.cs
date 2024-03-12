using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

public class TransferSourceNotFoundException : BaseException
{
    public TransferSourceNotFoundException(string? message) : base(message)
    {
    }

    public TransferSourceNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}