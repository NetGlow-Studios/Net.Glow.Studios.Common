namespace Net.Glow.Studios.Common.Notify.Exceptions;

public class NotifySmtpReceiverException : Exception
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