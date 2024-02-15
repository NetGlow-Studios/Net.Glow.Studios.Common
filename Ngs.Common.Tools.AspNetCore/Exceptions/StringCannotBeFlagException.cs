namespace Ngs.Common.Tools.AspNetCore.Exceptions;

public class StringCannotBeFlagException : Exception
{
    public StringCannotBeFlagException(string? message) : base(message)
    {
    }

    public StringCannotBeFlagException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}