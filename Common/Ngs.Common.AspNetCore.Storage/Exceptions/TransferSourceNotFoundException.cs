using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

/// <summary>
/// Thrown when the source of a transfer operation is not found.
/// </summary>
public class TransferSourceNotFoundException : BaseException
{
    public TransferSourceNotFoundException(string? message) : base(message)
    {
    }

    public TransferSourceNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}