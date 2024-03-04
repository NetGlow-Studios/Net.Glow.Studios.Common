using System.Net;

namespace Ngs.Common.Tools.Conversion;

/// <summary>
/// Provides methods for converting between network types.
/// </summary>
public static class NetworkConverter
{
    /// <summary>
    /// Converts the specified IP address to a binary string.
    /// </summary>
    /// <param name="ipAddress"> The IP address to convert. </param>
    /// <returns> The binary string representation of the IP address. </returns>
    public static string ConvertIpAddressToBinary(IPAddress ipAddress)
    {
        return ConvertIpAddressToBinary(ipAddress.GetAddressBytes());
    }
    
    /// <summary>
    /// Converts the specified IP address to a binary string.
    /// </summary>
    /// <param name="addressIp"> The IP address to convert. </param>
    /// <returns> The binary string representation of the IP address. </returns>
    public static string ConvertIpAddressToBinary(string addressIp)
    {
        return IPAddress.TryParse(addressIp, out var address) 
            ? ConvertIpAddressToBinary(address) 
            : string.Empty;
    }
    
    /// <summary>
    ///  Converts the specified IP address to a binary string.
    /// </summary>
    /// <param name="bytes"> The IP address to convert. </param>
    /// <returns> The binary string representation of the IP address. </returns>
    public static string ConvertIpAddressToBinary(IEnumerable<byte> bytes)
    {
        var binary = bytes.Aggregate("", (current, b) 
            => current + (Convert.ToString(b, 2).PadLeft(8, '0') + "."));
        return binary.TrimEnd('.');
    }

    /// <summary>
    /// Converts the specified binary string to an IP address.
    /// </summary>
    /// <param name="binary"> The binary string to convert. </param>
    /// <returns> The IP address representation of the binary string. </returns>
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
    
    /// <summary>
    ///  Converts the specified IPv6 address to a binary string.
    /// </summary>
    /// <param name="ipv6Address"> The IPv6 address to convert. </param>
    /// <returns> The binary string representation of the IPv6 address. </returns>
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
    
    /// <summary>
    /// Converts the specified binary string to an IPv6 address.
    /// </summary>
    /// <param name="binaryIPv6"> The binary string to convert. </param>
    /// <returns> The IPv6 address representation of the binary string. </returns>
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