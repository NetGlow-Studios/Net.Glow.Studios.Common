namespace Ngs.Common.Tools.Conversion.Physics;

public class SpeedConverter
{
    public static double KilometersPerHourToMilesPerHour(double kmph)
    {
        return kmph / 1.60934; // 1 mile = 1.60934 kilometers
    }

    public static double MilesPerHourToKilometersPerHour(double mph)
    {
        return mph * 1.60934; // 1 mile = 1.60934 kilometers
    }
    
    public static double MetersPerSecondToKilometersPerHour(double metersPerSecond)
    {
        return metersPerSecond * 3.6; // 1 meter/second = 3.6 kilometers/hour
    }

    public static double KilometersPerHourToMetersPerSecond(double kilometersPerHour)
    {
        return kilometersPerHour / 3.6; // 1 meter/second = 3.6 kilometers/hour
    }

    public static double MilesPerHourToMetersPerSecond(double milesPerHour)
    {
        return milesPerHour * 0.44704; // 1 mile/hour = 0.44704 meters/second
    }

    public static double MetersPerSecondToMilesPerHour(double metersPerSecond)
    {
        return metersPerSecond / 0.44704; // 1 mile/hour = 0.44704 meters/second
    }

}