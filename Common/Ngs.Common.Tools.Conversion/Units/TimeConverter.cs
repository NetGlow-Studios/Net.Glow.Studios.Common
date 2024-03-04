namespace Ngs.Common.Tools.Conversion.Units;

public class TimeConverter
{
    public static double MinutesToHours(double minutes)
    {
        return minutes / 60; // 1 hour = 60 minutes
    }

    public static double HoursToMinutes(double hours)
    {
        return hours * 60; // 1 hour = 60 minutes
    }

    public static double DaysToHours(double days)
    {
        return days * 24; // 1 day = 24 hours
    }

    public static double HoursToDays(double hours)
    {
        return hours / 24; // 1 day = 24 hours
    }
    
    public static double SecondsToMinutes(double seconds)
    {
        return seconds / 60; // 1 minute = 60 seconds
    }

    public static double MinutesToSeconds(double minutes)
    {
        return minutes * 60; // 1 minute = 60 seconds
    }

    public static double HoursToSeconds(double hours)
    {
        return hours * 3600; // 1 hour = 3600 seconds
    }

    public static double SecondsToHours(double seconds)
    {
        return seconds / 3600; // 1 hour = 3600 seconds
    }

    public static double DaysToSeconds(double days)
    {
        return days * 86400; // 1 day = 86400 seconds
    }

    public static double SecondsToDays(double seconds)
    {
        return seconds / 86400; // 1 day = 86400 seconds
    }
}