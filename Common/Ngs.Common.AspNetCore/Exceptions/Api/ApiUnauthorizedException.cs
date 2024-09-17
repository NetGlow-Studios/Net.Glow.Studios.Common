using System.Net;

namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiUnauthorizedException : BaseApiException
{
    public ApiUnauthorizedException(string? message) : base(message)
    {
    }

    public ApiUnauthorizedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
    public static void ThrowIfUnauthorized(HttpStatusCode statusCode, string message, Exception? innerException = null)
    {
        if(statusCode != HttpStatusCode.Unauthorized) return;
        
        throw new ApiUnauthorizedException(message, innerException);
    }
}