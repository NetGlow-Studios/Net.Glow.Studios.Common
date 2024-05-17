using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

public class NotSupportedExtensionCastException : BaseException
{
    public NotSupportedExtensionCastException(string? message) : base(message)
    {
    }

    public NotSupportedExtensionCastException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}