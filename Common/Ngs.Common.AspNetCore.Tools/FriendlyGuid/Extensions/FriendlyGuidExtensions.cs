namespace Ngs.Common.AspNetCore.Tools.FriendlyGuid.Extensions;

public static class FriendlyGuidExtensions
{
    public static Guid ToGuid(this FriendlyGuid friendlyGuid)
    {
        return Guid.Parse(friendlyGuid.ToString());
    }
    
    public static FriendlyGuid ToFriendlyGuid(this Guid guid)
    {
        return new FriendlyGuid(guid);
    }
}