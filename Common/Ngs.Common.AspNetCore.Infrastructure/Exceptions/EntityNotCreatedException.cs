using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.Exceptions;

/// <summary>
/// Thrown when entity was not created in repository
/// </summary>
public class EntityNotCreatedException : BaseException
{
    public EntityNotCreatedException()
    {
    }

    public EntityNotCreatedException(string? message) : base(message)
    {
    }
    
    public EntityNotCreatedException(Exception innerException) : base(innerException.Message, innerException)
    {
    }

    public EntityNotCreatedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}