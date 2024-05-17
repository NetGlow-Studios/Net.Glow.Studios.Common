using Ngs.Common.AspNetCore.Storage.Manager;

namespace Ngs.Common.AspNetCore.Storage.Tests.Storage;

public sealed class LocalContentManager : StorageLocalManager
{
    public LocalContentManager(string rootPath) : base(rootPath)
    {
    }
}