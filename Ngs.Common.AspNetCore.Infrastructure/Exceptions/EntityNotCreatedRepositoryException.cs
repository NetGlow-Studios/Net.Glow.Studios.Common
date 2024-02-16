namespace Ngs.Common.AspNetCore.Infrastructure.Exceptions;

public class EntityNotCreatedRepositoryException : Exception
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