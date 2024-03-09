using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.Exceptions;

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