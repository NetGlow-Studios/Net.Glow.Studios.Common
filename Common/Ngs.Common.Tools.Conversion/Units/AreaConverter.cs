namespace Ngs.Common.Tools.Conversion.Units;

public class AreaConverter
{
    public static double SquareMetersToSquareFeet(double squareMeters)
    {
        return squareMeters * 10.7639; // 1 square meter = 10.7639 square feet
    }

    public static double SquareFeetToSquareMeters(double squareFeet)
    {
        return squareFeet / 10.7639; // 1 square meter = 10.7639 square feet
    }
    
    public static double SquareKilometersToSquareMeters(double squareKilometers)
    {
        return squareKilometers * 1_000_000; // 1 square kilometer = 1,000,000 square meters
    }

    public static double SquareMetersToSquareKilometers(double squareMeters)
    {
        return squareMeters / 1_000_000; // 1 square kilometer = 1,000,000 square meters
    }

    public static double AcresToSquareMeters(double acres)
    {
        return acres * 4046.86; // 1 acre = 4046.86 square meters
    }

    public static double SquareMetersToAcres(double squareMeters)
    {
        return squareMeters / 4046.86; // 1 acre = 4046.86 square meters
    }

    public static double HectaresToAcres(double hectares)
    {
        return hectares * 2.47105; // 1 hectare = 2.47105 acres
    }

    public static double AcresToHectares(double acres)
    {
        return acres / 2.47105; // 1 hectare = 2.47105 acres
    }

}