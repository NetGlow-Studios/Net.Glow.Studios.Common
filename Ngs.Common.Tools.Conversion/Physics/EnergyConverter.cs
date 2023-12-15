namespace Ngs.Common.Tools.Conversion.Physics;

public class EnergyConverter
{
    public static double JoulesToCalories(double joules)
    {
        return joules * 0.239006; // 1 joule = 0.239006 calories
    }

    public static double CaloriesToJoules(double calories)
    {
        return calories / 0.239006; // 1 joule = 0.239006 calories
    }

    public static double JoulesToKilojoules(double joules)
    {
        return joules / 1000; // 1 kilojoule = 1000 joules
    }

    public static double KilojoulesToJoules(double kilojoules)
    {
        return kilojoules * 1000; // 1 kilojoule = 1000 joules
    }
    
    public static double JoulesToElectronVolts(double joules)
    {
        return joules * 6.242e+18; // 1 joule = 6.242e+18 electron volts
    }

    public static double ElectronVoltsToJoules(double electronVolts)
    {
        return electronVolts / 6.242e+18; // 1 joule = 6.242e+18 electron volts
    }

    public static double JoulesToBritishThermalUnits(double joules)
    {
        return joules * 0.000947817; // 1 joule = 0.000947817 BTUs
    }

    public static double BritishThermalUnitsToJoules(double btus)
    {
        return btus / 0.000947817; // 1 joule = 0.000947817 BTUs
    }
}