namespace Ngs.Common.Tools.Conversion.IT;

public class DataTransferRateConverter
{
    public static double MegabitsPerSecondToMegabytesPerSecond(double mbps)
    {
        return mbps * 0.125; // 1 megabyte/second = 8 megabits/second
    }

    public static double MegabytesPerSecondToMegabitsPerSecond(double mBps)
    {
        return mBps * 8; // 1 megabyte/second = 8 megabits/second
    }
    
    public static double KilobytesPerSecondToMegabitsPerSecond(double kBps)
    {
        return kBps * 0.008; // 1 megabit/second = 125 kilobytes/second
    }

    public static double MegabitsPerSecondToKilobytesPerSecond(double mbps)
    {
        return mbps * 125; // 1 megabit/second = 125 kilobytes/second
    }

    public static double MegabytesPerSecondToGigabitsPerSecond(double MBps)
    {
        return MBps * 0.008; // 1 gigabit/second = 125 megabytes/second
    }

    public static double GigabitsPerSecondToMegabytesPerSecond(double Gbps)
    {
        return Gbps * 125; // 1 gigabit/second = 125 megabytes/second
    }

}