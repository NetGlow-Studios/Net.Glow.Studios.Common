namespace Ngs.Common.AspNetCore.DataSower.Exceptions;

/// <summary>
/// Exception thrown when unique property is not unique, or the unique property is not defined in the dataSeed.
/// </summary>
public class UniquePropException : Exception
{
    public UniquePropException(string? message) : base(message)
    {
    }

    public UniquePropException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}