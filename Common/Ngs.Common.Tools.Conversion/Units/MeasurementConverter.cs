namespace Ngs.Common.Tools.Conversion.Units;

public class MeasurementConverter
{
    public static double InchesToCentimeters(double inches)
    {
        return inches * 2.54; // 1 inch = 2.54 centimeters
    }

    public static double CentimetersToInches(double centimeters)
    {
        return centimeters / 2.54; // 1 centimeter = 0.393701 inches
    }

    public static double FeetToMeters(double feet)
    {
        return feet * 0.3048; // 1 foot = 0.3048 meters
    }

    public static double MetersToFeet(double meters)
    {
        return meters / 0.3048; // 1 meter = 3.28084 feet
    }
    
    public static double MilesToKilometers(double miles)
    {
        return miles * 1.60934; // 1 mile = 1.60934 kilometers
    }

    public static double KilometersToMiles(double kilometers)
    {
        return kilometers / 1.60934; // 1 kilometer = 0.621371 miles
    }
}