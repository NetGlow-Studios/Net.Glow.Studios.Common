namespace Ngs.Common.Tools.Conversion;

public class ByteStorageConverter
{
    public static double BytesToKilobytes(double bytes)
    {
        return bytes / 1024; // 1 kilobyte = 1024 bytes
    }

    public static double KilobytesToBytes(double kilobytes)
    {
        return kilobytes * 1024; // 1 kilobyte = 1024 bytes
    }

    public static double MegabytesToGigabytes(double megabytes)
    {
        return megabytes / 1024; // 1 gigabyte = 1024 megabytes
    }

    public static double GigabytesToMegabytes(double gigabytes)
    {
        return gigabytes * 1024; // 1 gigabyte = 1024 megabytes
    }

    public static double BytesToMegabytes(double bytes)
    {
        return bytes / (1024 * 1024); // 1 megabyte = 1024 * 1024 bytes
    }

    public static double MegabytesToBytes(double megabytes)
    {
        return megabytes * (1024 * 1024); // 1 megabyte = 1024 * 1024 bytes
    }

    public static double GigabytesToTerabytes(double gigabytes)
    {
        return gigabytes / 1024; // 1 terabyte = 1024 gigabytes
    }

    public static double TerabytesToGigabytes(double terabytes)
    {
        return terabytes * 1024; // 1 terabyte = 1024 gigabytes
    }

}