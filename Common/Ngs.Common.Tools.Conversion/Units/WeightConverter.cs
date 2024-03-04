namespace Ngs.Common.Tools.Conversion.Units;

public class WeightConverter
{
    public static double PoundsToKilograms(double pounds)
    {
        return pounds * 0.453592; // 1 pound = 0.453592 kilograms
    }

    public static double KilogramsToPounds(double kilograms)
    {
        return kilograms * 2.20462; // 1 kilogram = 2.20462 pounds
    }

    public static double OuncesToGrams(double ounces)
    {
        return ounces * 28.3495; // 1 ounce = 28.3495 grams
    }

    public static double GramsToOunces(double grams)
    {
        return grams * 0.035274; // 1 gram = 0.035274 ounces
    }
}