using System.Net;

namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiNotEstablishedConnectionException : BaseApiException
{
    public ApiNotEstablishedConnectionException(string? message) : base(message)
    {
    }

    public ApiNotEstablishedConnectionException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
    public static void ThrowIfNotEstablishedConnection(HttpStatusCode statusCode, string message, Exception? innerException = null)
    {
        if(statusCode != HttpStatusCode.NotFound) return;
        
        throw new ApiNotEstablishedConnectionException(message, innerException);
    }
}