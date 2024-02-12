using Ngs.Gliwice.Church.Domain.Exceptions.Base;

namespace Net.Glow.Studios.Infrastructure.Exceptions;

public class EntityNotFoundRepositoryException : BaseNotFoundException
{
    public EntityNotFoundRepositoryException(string? message) : base(message)
    {
    }

    public EntityNotFoundRepositoryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public EntityNotFoundRepositoryException(Guid? id, string objName, string? message) : base(id, objName, message)
    {
    }

    public EntityNotFoundRepositoryException(Guid? id, string objName, string? message, Exception innerException) : base(id, objName, message, innerException)
    {
    }
}