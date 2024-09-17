namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiNoConnectionException : BaseApiException
{
    public ApiNoConnectionException(string? message) : base(message)
    {
    }

    public ApiNoConnectionException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}