using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.Exceptions;
/// <summary>
/// Exception for when an entity is not found in the repository
/// </summary>
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