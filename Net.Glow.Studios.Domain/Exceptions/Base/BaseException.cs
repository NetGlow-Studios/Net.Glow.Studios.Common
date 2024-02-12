namespace Ngs.Gliwice.Church.Domain.Exceptions.Base;

public abstract class BaseException : Exception
{
    protected BaseException()
    {
    }

    protected BaseException(string? message) : base(message)
    {
    }

    protected BaseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}