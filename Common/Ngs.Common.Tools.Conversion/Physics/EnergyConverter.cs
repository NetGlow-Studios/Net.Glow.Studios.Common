namespace Ngs.Common.Tools.Conversion.Physics;

/// <summary>
/// Class for converting energy units.
/// </summary>
public class EnergyConverter
{
    /// <summary>
    /// Converts joules to calories.
    /// </summary>
    /// <param name="joules"> The amount of joules to convert. </param>
    /// <returns> The amount of calories. </returns>
    public static double JoulesToCalories(double joules)
    {
        return joules * 0.239006; // 1 joule = 0.239006 calories
    }

    /// <summary>
    /// Converts calories to joules.
    /// </summary>
    /// <param name="calories"> The amount of calories to convert. </param>
    /// <returns> The amount of joules. </returns>
    public static double CaloriesToJoules(double calories)
    {
        return calories / 0.239006; // 1 joule = 0.239006 calories
    }

    /// <summary>
    /// Converts joules to kilojoules.
    /// </summary>
    /// <param name="joules"> The amount of joules to convert. </param>
    /// <returns> The amount of kilojoules. </returns>
    public static double JoulesToKilojoules(double joules)
    {
        return joules / 1000; // 1 kilojoule = 1000 joules
    }

    /// <summary>
    /// Converts kilojoules to joules.
    /// </summary>
    /// <param name="kilojoules"> The amount of kilojoules to convert. </param>
    /// <returns> The amount of joules. </returns>
    public static double KilojoulesToJoules(double kilojoules)
    {
        return kilojoules * 1000; // 1 kilojoule = 1000 joules
    }
    
    /// <summary>
    /// Converts joules to electron volts.
    /// </summary>
    /// <param name="joules"> The amount of joules to convert. </param>
    /// <returns> The amount of electron volts. </returns>
    public static double JoulesToElectronVolts(double joules)
    {
        return joules * 6.242e+18; // 1 joule = 6.242e+18 electron volts
    }

    /// <summary>
    /// Converts electron volts to joules.
    /// </summary>
    /// <param name="electronVolts"> The amount of electron volts to convert. </param>
    /// <returns> The amount of joules. </returns>
    public static double ElectronVoltsToJoules(double electronVolts)
    {
        return electronVolts / 6.242e+18; // 1 joule = 6.242e+18 electron volts
    }

    /// <summary>
    /// Converts joules to British thermal units.
    /// </summary>
    /// <param name="joules"> The amount of joules to convert. </param>
    /// <returns> The amount of British thermal units. </returns>
    public static double JoulesToBritishThermalUnits(double joules)
    {
        return joules * 0.000947817; // 1 joule = 0.000947817 BTUs
    }

    /// <summary>
    /// Converts British thermal units to joules.
    /// </summary>
    /// <param name="btus"> The amount of British thermal units to convert. </param>
    /// <returns> The amount of joules. </returns>
    public static double BritishThermalUnitsToJoules(double btus)
    {
        return btus / 0.000947817; // 1 joule = 0.000947817 BTUs
    }
}