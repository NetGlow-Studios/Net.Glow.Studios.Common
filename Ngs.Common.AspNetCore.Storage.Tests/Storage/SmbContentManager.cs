using Ngs.Common.AspNetCore.Storage.Manager;

namespace Ngs.Common.AspNetCore.Storage.Tests.Storage;

public class SmbContentManager : StorageSmbManager
{
    public SmbContentManager(string username, string password, string localIp) : base(username, password, localIp)
    {
    }
}