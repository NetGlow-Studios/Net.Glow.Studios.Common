using System.Net;

namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiBadGatewayException : BaseApiException
{
    public ApiBadGatewayException(string? message) : base(message)
    {
    }

    public ApiBadGatewayException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public static void ThrowIfBadGateway(HttpStatusCode statusCode, string message, Exception? innerException = null)
    {
        if(statusCode != HttpStatusCode.BadGateway) return;
        
        throw new ApiBadGatewayException(message, innerException);
    }
}