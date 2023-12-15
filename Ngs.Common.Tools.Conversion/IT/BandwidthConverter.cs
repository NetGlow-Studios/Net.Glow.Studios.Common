namespace Ngs.Common.Tools.Conversion.IT;

public class BandwidthConverter
{
    public static double MegabitsToKilobitsPerSecond(double megabits)
    {
        return megabits * 1000; // 1 megabit/second = 1000 kilobits/second
    }

    public static double KilobitsToMegabitsPerSecond(double kilobits)
    {
        return kilobits / 1000; // 1 megabit/second = 1000 kilobits/second
    }

    public static double MegabitsToGigabitsPerSecond(double megabits)
    {
        return megabits / 1000; // 1 gigabit/second = 1000 megabits/second
    }

    public static double GigabitsToMegabitsPerSecond(double gigabits)
    {
        return gigabits * 1000; // 1 gigabit/second = 1000 megabits/second
    }

    public static double BytesToBits(double bytes)
    {
        return bytes * 8; // 1 megabyte = 8 megabits
    }

    public static double BitsToBytes(double bits)
    {
        return bits / 8; // 1 megabyte = 8 megabits
    }
}