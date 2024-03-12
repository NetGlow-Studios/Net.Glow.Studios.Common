using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.Exceptions;

/// <summary>
/// Exception that is thrown when connection to the database is not established.
/// </summary>
public class ConnectionNotEstablishedException : BaseException 
{
    public ConnectionNotEstablishedException()
    {
    }

    public ConnectionNotEstablishedException(string? message) : base(message)
    {
    }

    public ConnectionNotEstablishedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}