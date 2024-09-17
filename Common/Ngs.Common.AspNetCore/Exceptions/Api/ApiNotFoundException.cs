namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiNotFoundException : BaseApiException
{
    public ApiNotFoundException(string? message) : base(message)
    {
    }

    public ApiNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}