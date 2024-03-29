using System.Net;

namespace Ngs.Common.Tools.Network;

public static class IpAddress
{
    /// <summary>
    /// Get the local IP address of the machine.
    /// </summary>
    /// <returns></returns>
    public static string GetMyIp()
    {
        var hostName = Dns.GetHostName();
        var ipAddresses = Dns.GetHostAddresses(hostName);

        foreach (var ipAddress in ipAddresses)
        {
            if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ipAddress.ToString();
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// Get the public IP address of the machine.
    /// </summary>
    /// <returns></returns>
    public static string GetMyPublicIp()
    {
        var externalIpString = new HttpClient().GetStringAsync("https://icanhazip.com").Result;
        
        externalIpString = externalIpString?.Replace(@"\r\n", "").Replace("\\n", "").Trim();

        return !IPAddress.TryParse(externalIpString, out var ipAddress)
            ? string.Empty
            : ipAddress.ToString();
    }

    public static void AnalyzeIpAddress(string ipaddress)
    {
    }
}