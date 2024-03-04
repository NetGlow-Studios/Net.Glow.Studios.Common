namespace Ngs.Common.Tools.Conversion.Physics;

public class SpeedConverter
{
    /// <summary>
    /// Converts kilometers per hour to miles per hour.
    /// </summary>
    /// <param name="kmph"> Kilometers per hour. </param>
    /// <returns> Meters per second. </returns>
    public static double KilometersPerHourToMilesPerHour(double kmph)
    {
        return kmph / 1.60934; // 1 mile = 1.60934 kilometers
    }

    /// <summary>
    ///  Converts miles per hour to kilometers per hour.
    /// </summary>
    /// <param name="mph"></param>
    /// <returns></returns>
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