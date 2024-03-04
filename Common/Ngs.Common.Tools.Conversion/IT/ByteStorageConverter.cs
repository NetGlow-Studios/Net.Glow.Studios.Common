namespace Ngs.Common.Tools.Conversion.IT;

/// <summary>
/// Class for converting byte storage.
/// </summary>
public class ByteStorageConverter
{
    /// <summary>
    /// Convert bytes to kilobytes.
    /// </summary>
    /// <param name="bytes"> Bytes to convert. </param>
    /// <returns> Kilobytes. </returns>
    public static double BytesToKilobytes(double bytes)
    {
        return bytes / 1024; // 1 kilobyte = 1024 bytes
    }

    /// <summary>
    /// Convert kilobytes to bytes.
    /// </summary>
    /// <param name="kilobytes"> Kilobytes to convert. </param>
    /// <returns> Bytes. </returns>
    public static double KilobytesToBytes(double kilobytes)
    {
        return kilobytes * 1024; // 1 kilobyte = 1024 bytes
    }

    /// <summary>
    /// Convert kilobytes to megabytes.
    /// </summary>
    /// <param name="megabytes"> Megabytes to convert. </param>
    /// <returns> Megabytes. </returns>
    public static double MegabytesToGigabytes(double megabytes)
    {
        return megabytes / 1024; // 1 gigabyte = 1024 megabytes
    }

    /// <summary>
    /// Convert gigabytes to megabytes.
    /// </summary>
    /// <param name="gigabytes"> Gigabytes to convert. </param>
    /// <returns> Megabytes. </returns>
    public static double GigabytesToMegabytes(double gigabytes)
    {
        return gigabytes * 1024; // 1 gigabyte = 1024 megabytes
    }

    /// <summary>
    /// Convert bytes to megabytes.
    /// </summary>
    /// <param name="bytes"> Bytes to convert. </param>
    /// <returns> Megabytes. </returns>
    public static double BytesToMegabytes(double bytes)
    {
        return bytes / (1024 * 1024); // 1 megabyte = 1024 * 1024 bytes
    }

    /// <summary>
    /// Convert megabytes to bytes.
    /// </summary>
    /// <param name="megabytes"> Megabytes to convert. </param>
    /// <returns> Bytes. </returns>
    public static double MegabytesToBytes(double megabytes)
    {
        return megabytes * (1024 * 1024); // 1 megabyte = 1024 * 1024 bytes
    }

    /// <summary>
    /// Convert megabytes to gigabytes.
    /// </summary>
    /// <param name="gigabytes"> Gigabytes to convert. </param>
    /// <returns> Gigabytes. </returns>
    public static double GigabytesToTerabytes(double gigabytes)
    {
        return gigabytes / 1024; // 1 terabyte = 1024 gigabytes
    }

    /// <summary>
    /// Convert gigabytes to terabytes.
    /// </summary>
    /// <param name="terabytes"> Terabytes to convert. </param>
    /// <returns> Terabytes. </returns>
    public static double TerabytesToGigabytes(double terabytes)
    {
        return terabytes * 1024; // 1 terabyte = 1024 gigabytes
    }

}