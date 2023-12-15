namespace Ngs.Common.Tools.Conversion.Physics;

public class PowerConverter
{
    public static double WattsToHorsepower(double watts)
    {
        return watts / 745.7; // 1 horsepower = 745.7 watts
    }

    public static double HorsepowerToWatts(double horsepower)
    {
        return horsepower * 745.7; // 1 horsepower = 745.7 watts
    }
    
    public static double WattsToKilowatts(double watts)
    {
        return watts / 1000; // 1 kilowatt = 1000 watts
    }

    public static double KilowattsToWatts(double kilowatts)
    {
        return kilowatts * 1000; // 1 kilowatt = 1000 watts
    }

    public static double WattsToMegawatts(double watts)
    {
        return watts / 1_000_000; // 1 megawatt = 1,000,000 watts
    }

    public static double MegawattsToWatts(double megawatts)
    {
        return megawatts * 1_000_000; // 1 megawatt = 1,000,000 watts
    }
}