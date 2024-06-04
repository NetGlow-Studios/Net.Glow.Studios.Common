using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Mongo.Infrastructure.Exceptions;

/// <summary>
/// Thrown when entity was not created in repository
/// </summary>
public class EntityNotCreatedRepositoryException : BaseException
{
    public EntityNotCreatedRepositoryException()
    {
    }

    public EntityNotCreatedRepositoryException(string? message) : base(message)
    {
    }
    
    public EntityNotCreatedRepositoryException(Exception innerException) : base(innerException.Message, innerException)
    {
    }

    public EntityNotCreatedRepositoryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}