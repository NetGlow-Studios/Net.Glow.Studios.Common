using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.DataSower.Exceptions;

public class NotExistedDbContextException : BaseException
{
    public NotExistedDbContextException(string? message) : base(message)
    {
    }

    public NotExistedDbContextException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}