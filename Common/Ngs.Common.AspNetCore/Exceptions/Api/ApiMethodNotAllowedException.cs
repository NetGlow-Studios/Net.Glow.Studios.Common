using System.Net;

namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiMethodNotAllowedException : BaseApiException
{
    public ApiMethodNotAllowedException(string? message) : base(message)
    {
    }

    public ApiMethodNotAllowedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
    public static void ThrowIfMethodNotAllowed(HttpStatusCode statusCode, string message, Exception? innerException = null)
    {
        if(statusCode != HttpStatusCode.MethodNotAllowed) return;
        
        throw new ApiMethodNotAllowedException(message, innerException);
    }
}