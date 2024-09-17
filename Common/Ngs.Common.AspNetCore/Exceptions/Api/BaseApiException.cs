using System.Net;

namespace Ngs.Common.AspNetCore.Exceptions.Api;

public abstract class BaseApiException : BaseException
{
    protected BaseApiException()
    {
    }

    protected BaseApiException(string? message) : base(message)
    {
    }

    protected BaseApiException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
    
    public static void ThrowIfNotSuccess(HttpResponseMessage response)
    {
        if((int)response.StatusCode >= 100 && (int)response.StatusCode < 300) return;

        throw response.StatusCode switch
        {
            HttpStatusCode.BadRequest => // 400
                new ApiBadRequestException($"Bad request: {response.ReasonPhrase}"),
            HttpStatusCode.Unauthorized => // 401
                new ApiUnauthorizedException($"Unauthorized: {response.ReasonPhrase}"),
            HttpStatusCode.PaymentRequired => // 402
                new ApiPaymentRequiredException("Payment required"),
            HttpStatusCode.Forbidden => // 403
                new ApiForbiddenException("Forbidden"),
            HttpStatusCode.NotFound => // 404
                new ApiNotFoundException("Not found"),
            HttpStatusCode.MethodNotAllowed => // 405
                new ApiMethodNotAllowedException($"Method not allowed: {response.ReasonPhrase}"),
            HttpStatusCode.BadGateway => // 502
                new ApiBadGatewayException("Bad gateway"),
            _ => new HttpRequestException($"Unhandled status code: {response.StatusCode}")
        };
    }
}