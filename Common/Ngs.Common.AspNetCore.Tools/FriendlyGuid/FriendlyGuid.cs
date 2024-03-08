namespace Ngs.Common.AspNetCore.Tools.FriendlyGuid;

public class FriendlyGuid
{
    public Guid Guid { get; }
    
    public FriendlyGuid(Guid guid)
    {
        Guid = guid;
    }

    public override string ToString()
    {
        return "";
    }
}