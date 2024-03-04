namespace Ngs.Common.Tools.Conversion.Units;

public class SpatialVolumeConverter
{
    public static double LitersToGallons(double liters)
    {
        return liters * 0.264172; // 1 liter = 0.264172 gallons
    }

    public static double GallonsToLiters(double gallons)
    {
        return gallons * 3.78541; // 1 gallon = 3.78541 liters
    }
}