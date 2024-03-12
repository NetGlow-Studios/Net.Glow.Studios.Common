using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

public class BackupNotFoundException : BaseNotFoundException
{
    public BackupNotFoundException(string? message) : base(message)
    {
        
    }

    public BackupNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public BackupNotFoundException(Guid? id, string objName, string? message) : base(id, objName, message)
    {
    }

    public BackupNotFoundException(Guid? id, string objName, string? message, Exception innerException) : base(id, objName, message, innerException)
    {
    }
}