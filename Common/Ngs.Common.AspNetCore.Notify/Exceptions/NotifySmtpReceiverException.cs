using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Notify.Exceptions;

/// <summary>
/// Exception thrown when there is an error in the NotifySmtpReceiver.
/// </summary>
public class NotifySmtpReceiverException : BaseException
{
    public NotifySmtpReceiverException(string? message) : base(message)
    {
    }

    public NotifySmtpReceiverException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}