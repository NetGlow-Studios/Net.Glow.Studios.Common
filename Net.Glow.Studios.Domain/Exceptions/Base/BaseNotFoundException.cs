namespace Ngs.Gliwice.Church.Domain.Exceptions.Base;

/// <summary>
/// Not found exception.
/// </summary>
public abstract class BaseNotFoundException : BaseException
{
    protected BaseNotFoundException()
    {
    }

    protected BaseNotFoundException(string? message) : base(message)
    {
    }

    protected BaseNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Not found Exception
    /// </summary>
    /// <param name="id">Id referenced to entity in database.</param>
    /// <param name="objName">Object name to specify the object with a problem.</param>
    /// <param name="message">Additional message.</param>
    protected BaseNotFoundException(Guid? id, string objName, string? message) 
        : base($"{objName} with id: '{id}' not found. {message}")
    {
    }
    
    /// <summary>
    /// Not found Exception
    /// </summary>
    /// <param name="id">Id referenced to entity in database.</param>
    /// <param name="objName">Object name to specify the object with a problem.</param>
    /// <param name="message">Additional message.</param>
    /// <param name="innerException">Inner Exception.</param>
    protected BaseNotFoundException(Guid? id, string objName, string? message, Exception innerException) 
        : base($"{objName} with id: '{id}' not found. {message}", innerException)
    {
    }
}