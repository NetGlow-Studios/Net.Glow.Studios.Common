namespace Ngs.Common.Tools.Conversion.IT;

/// <summary>
/// Bandwidth converter.
/// </summary>
public class BandwidthConverter
{
    /// <summary>
    /// Convert megabits to kilobits per second.
    /// </summary>
    /// <param name="megabits"> Megabits. </param>
    /// <returns> Kilobits per second. </returns>
    public static double MegabitsToKilobitsPerSecond(double megabits)
    {
        return megabits * 1000; // 1 megabit/second = 1000 kilobits/second
    }

    /// <summary>
    ///  Convert kilobits to megabits per second.
    /// </summary>
    /// <param name="kilobits"> Kilobits. </param>
    /// <returns> Megabits per second. </returns>
    public static double KilobitsToMegabitsPerSecond(double kilobits)
    {
        return kilobits / 1000; // 1 megabit/second = 1000 kilobits/second
    }

    /// <summary>
    /// Convert megabits to gigabits per second.
    /// </summary>
    /// <param name="megabits"> Megabits. </param>
    /// <returns> Gigabits per second. </returns>
    public static double MegabitsToGigabitsPerSecond(double megabits)
    {
        return megabits / 1000; // 1 gigabit/second = 1000 megabits/second
    }

    /// <summary>
    ///  Convert gigabits to megabits per second.
    /// </summary>
    /// <param name="gigabits"> Gigabits. </param>
    /// <returns> Megabits per second. </returns>
    public static double GigabitsToMegabitsPerSecond(double gigabits)
    {
        return gigabits * 1000; // 1 gigabit/second = 1000 megabits/second
    }

    /// <summary>
    ///  Convert megabits to bytes.
    /// </summary>
    /// <param name="bytes"> Bytes. </param>
    /// <returns> Megabits. </returns>
    public static double BytesToBits(double bytes)
    {
        return bytes * 8; // 1 megabyte = 8 megabits
    }

    /// <summary>
    /// Convert bytes to megabits.
    /// </summary>
    /// <param name="bits"> Megabits. </param>
    /// <returns> Bytes. </returns>
    public static double BitsToBytes(double bits)
    {
        return bits / 8; // 1 megabyte = 8 megabits
    }
}