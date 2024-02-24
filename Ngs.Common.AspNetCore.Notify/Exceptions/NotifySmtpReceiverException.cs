using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.Notify.Exceptions;

/// <summary>
/// Exception thrown when there is an error in the NotifySmtpReceiver.
/// </summary>
public class NotifySmtpReceiverException : BaseException
{
    public NotifySmtpReceiverException()
    {
    }

    public NotifySmtpReceiverException(string? message) : base(message)
    {
    }

    public NotifySmtpReceiverException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}