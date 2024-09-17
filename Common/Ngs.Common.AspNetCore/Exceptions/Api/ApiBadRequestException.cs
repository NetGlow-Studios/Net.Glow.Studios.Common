namespace Ngs.Common.AspNetCore.Exceptions.Api;

public class ApiBadRequestException : BaseApiException
{
    public ApiBadRequestException(string? message) : base(message)
    {
    }

    public ApiBadRequestException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}