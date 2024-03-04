using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.AccessControl.Exceptions;

/// <summary>
/// Exception thrown when a string cannot be used as a flag.
/// </summary>
public class StringCannotBeFlagException : BaseException
{
    public StringCannotBeFlagException(string? message) : base(message)
    {
    }

    public StringCannotBeFlagException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}