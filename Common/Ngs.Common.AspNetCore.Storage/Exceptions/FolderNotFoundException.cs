using Ngs.Common.AspNetCore.Exceptions;

namespace Ngs.Common.AspNetCore.Storage.Exceptions;

public class FolderNotFoundException : BaseException
{
    public FolderNotFoundException(string? message) : base(message)
    {
    }

    public FolderNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}