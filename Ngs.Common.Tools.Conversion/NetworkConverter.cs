using System.Net;

namespace Ngs.Common.Tools.Conversion;

public static class NetworkConverter
{
    public static string ConvertIpAddressToBinary(IPAddress ipAddress)
    {
        return ConvertIpAddressToBinary(ipAddress.GetAddressBytes());
    }
    
    public static string ConvertIpAddressToBinary(string addressIp)
    {
        return IPAddress.TryParse(addressIp, out var address) 
            ? ConvertIpAddressToBinary(address) 
            : string.Empty;
    }
    
    public static string ConvertIpAddressToBinary(IEnumerable<byte> bytes)
    {
        var binary = bytes.Aggregate("", (current, b) 
            => current + (Convert.ToString(b, 2).PadLeft(8, '0') + "."));
        return binary.TrimEnd('.');
    }

    public static string ConvertBinaryToIpAddress(string binary)
    {
        var binaryParts = binary.Split('.');
        var bytes = new byte[4];

        for (var i = 0; i < 4; i++)
        {
            bytes[i] = Convert.ToByte(binaryParts[i], 2);
        }

        return new IPAddress(bytes).ToString();
    }
    
    public static string ConvertIPv6ToBinary(string ipv6Address)
    {
        var hexSegments = ipv6Address.Split(':');

        // Ensure proper IPv6 format (8 segments)
        if (hexSegments.Length != 8)
        {
            return string.Empty;
        }

        var binaryAddress = hexSegments
            .Select(segment => Convert.ToString(Convert.ToInt32(segment, 16), 2)
                .PadLeft(16, '0'))
            .Aggregate("", (current, binarySegment) => current + (binarySegment + ":"));

        return binaryAddress.Trim().TrimEnd(':');
    }
    
    public static string ConvertBinaryToIPv6(string binaryIPv6)
    {
        var binarySegments = binaryIPv6.Split(':');

        // Ensure proper binary format (8 segments of 16 bits)
        if (binarySegments.Length != 8)
        {
            return "Invalid Binary IPv6 Address";
        }

        var ipv6Address = binarySegments
            .Select(segment => Convert.ToString(Convert.ToInt32(segment, 2), 16)
                .PadLeft(4, '0'))
            .Aggregate("", (current, hexSegment) => current + (hexSegment + ":"));
        
        return ipv6Address.TrimEnd(':');
    }
}