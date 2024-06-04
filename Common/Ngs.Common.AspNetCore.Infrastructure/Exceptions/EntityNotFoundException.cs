using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Infrastructure.Exceptions;
/// <summary>
/// Exception for when an entity is not found in the repository
/// </summary>
public class EntityNotFoundException : BaseNotFoundException
{
    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public EntityNotFoundException(object? id, string objName, string? message) : base(id, objName, message)
    {
    }

    public EntityNotFoundException(object? id, string objName, string? message, Exception innerException) : base(id, objName, message, innerException)
    {
    }
}