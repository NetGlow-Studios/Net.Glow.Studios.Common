using System.Net;

namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiForbiddenException : BaseApiException
{
    public ApiForbiddenException(string? message) : base(message)
    {
    }

    public ApiForbiddenException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
    public static void ThrowIfForbidden(HttpStatusCode statusCode, string message, Exception? innerException = null)
    {
        if(statusCode != HttpStatusCode.Forbidden) return;
        
        throw new ApiForbiddenException(message, innerException);
    }
}