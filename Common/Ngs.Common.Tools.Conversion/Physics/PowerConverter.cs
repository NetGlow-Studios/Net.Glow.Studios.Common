namespace Ngs.Common.Tools.Conversion.Physics;

/// <summary>
/// Class for converting power units.
/// </summary>
public class PowerConverter
{
    /// <summary>
    /// Converts watts to horsepower.
    /// </summary>
    /// <param name="watts"> Power in watts. </param>
    /// <returns> Power in horsepower. </returns>
    public static double WattsToHorsepower(double watts)
    {
        return watts / 745.7; // 1 horsepower = 745.7 watts
    }

    /// <summary>
    /// Converts horsepower to watts.
    /// </summary>
    /// <param name="horsepower"> Power in horsepower. </param>
    /// <returns> Power in watts. </returns>
    public static double HorsepowerToWatts(double horsepower)
    {
        return horsepower * 745.7; // 1 horsepower = 745.7 watts
    }
    
    /// <summary>
    /// Converts watts to kilowatts.
    /// </summary>
    /// <param name="watts"> Power in watts. </param>
    /// <returns> Power in kilowatts. </returns>
    public static double WattsToKilowatts(double watts)
    {
        return watts / 1000; // 1 kilowatt = 1000 watts
    }

    /// <summary>
    /// Converts kilowatts to watts.
    /// </summary>
    /// <param name="kilowatts"> Power in kilowatts. </param>
    /// <returns> Power in watts. </returns>
    public static double KilowattsToWatts(double kilowatts)
    {
        return kilowatts * 1000; // 1 kilowatt = 1000 watts
    }

    /// <summary>
    /// Converts watts to megawatts.
    /// </summary>
    /// <param name="watts"> Power in watts. </param>
    /// <returns> Power in megawatts. </returns>
    public static double WattsToMegawatts(double watts)
    {
        return watts / 1_000_000; // 1 megawatt = 1,000,000 watts
    }

    /// <summary>
    /// Converts megawatts to watts.
    /// </summary>
    /// <param name="megawatts"> Power in megawatts. </param>
    /// <returns> Power in watts. </returns>
    public static double MegawattsToWatts(double megawatts)
    {
        return megawatts * 1_000_000; // 1 megawatt = 1,000,000 watts
    }
}