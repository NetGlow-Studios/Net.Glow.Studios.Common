namespace Ngs.Common.Tools.Conversion.Tests;

public class NetworkConverterTests
{
    [Fact]
    public void IpAddressToBinaryTest()
    {
        const string ipAddress = "192.168.0.1";
        const string expected = "11000000.10101000.00000000.00000001";

        var convertedValue = NetworkConverter.ConvertIpAddressToBinary(ipAddress);
        
        Assert.Equal(expected, convertedValue);
    }
    
    [Fact]
    public void BinaryToIpAddressTest()
    {
        const string expected = "192.168.0.1";
        const string binary = "11000000.10101000.00000000.00000001";

        var convertedValue = NetworkConverter.ConvertBinaryToIpAddress(binary);
        
        Assert.Equal(expected, convertedValue);
    }

    [Fact]
    public void IpAddressV6ToBinaryTest()
    {
        const string ipv6Address = "2001:0db8:85a3:0000:0000:8a2e:0370:7334";
        const string expected = "0010000000000001:0000110110111000:1000010110100011:0000000000000000:" +
                                "0000000000000000:1000101000101110:0000001101110000:0111001100110100";
        
        var binaryIPv6 = NetworkConverter.ConvertIPv6ToBinary(ipv6Address);
        
        Assert.Equal(expected, binaryIPv6);
    }
    
    [Fact]
    public void BinaryToIpAddressV6Test()
    {
        const string expected = "2001:0db8:85a3:0000:0000:8a2e:0370:7334";
        const string binary = "0010000000000001:0000110110111000:1000010110100011:0000000000000000:" +
                              "0000000000000000:1000101000101110:0000001101110000:0111001100110100";
        
        var iPv6 = NetworkConverter.ConvertBinaryToIPv6(binary);
        
        Assert.Equal(expected, iPv6);
    }
}