using Ngs.Common.AspNetCore.Exceptions.Base;

namespace Ngs.Common.Notify.Exceptions;

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