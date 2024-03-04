namespace Ngs.Common.Tools.Network.Tests;

public class IpAddressTests
{
    [Fact]
    public void MyIpTest()
    {
        var ipAddress = IpAddress.GetMyIp();

        Assert.NotSame(ipAddress, string.Empty);
    }

    [Fact]
    public void MyPublicIpTest()
    {
        var publicIp = IpAddress.GetMyPublicIp();
        
        Assert.NotSame(publicIp, string.Empty);
    }
}