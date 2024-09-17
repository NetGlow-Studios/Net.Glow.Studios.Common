using System.Net;

namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiPaymentRequiredException : BaseApiException
{
    public ApiPaymentRequiredException(string? message) : base(message)
    {
    }

    public ApiPaymentRequiredException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
    public static void ThrowIfPaymentRequired(HttpStatusCode statusCode, string message, Exception? innerException = null)
    {
        if(statusCode != HttpStatusCode.PaymentRequired) return;
        
        throw new ApiPaymentRequiredException(message, innerException);
    }
}