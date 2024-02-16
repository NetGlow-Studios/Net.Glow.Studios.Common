using Ngs.Common.AspNetCore.Exceptions.Base;

namespace Ngs.Common.AspNetCore.AccessControl.Exceptions;

public class StringCannotBeFlagException : BaseException
{
    public StringCannotBeFlagException(string? message) : base(message)
    {
    }

    public StringCannotBeFlagException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}