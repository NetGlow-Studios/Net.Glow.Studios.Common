namespace Ngs.Common.AspNetCore.Exceptions;

/// <summary>
/// Base exception for all exceptions in the application. To handle all exceptions in the application, use the middleware
/// </summary>
public abstract class BaseException : Exception
{
    /// <summary>
    /// Default constructor
    /// </summary>
    protected BaseException()
    {
    }

    /// <summary>
    /// Constructor with message
    /// </summary>
    /// <param name="message"></param>
    protected BaseException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Constructor with message and inner exception
    /// </summary>
    /// <param name="message"> </param>
    /// <param name="innerException"></param>
    protected BaseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}