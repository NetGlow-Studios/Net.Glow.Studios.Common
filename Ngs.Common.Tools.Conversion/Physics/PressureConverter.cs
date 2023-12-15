namespace Ngs.Common.Tools.Conversion.Physics;

public class PressureConverter
{
    public static double PascalsToPsi(double pascals)
    {
        return pascals * 0.000145038; // 1 PSI = 6894.76 Pascals
    }

    public static double PsiToPascals(double psi)
    {
        return psi / 0.000145038; // 1 PSI = 6894.76 Pascals
    }

    public static double PascalsToAtmospheres(double pascals)
    {
        return pascals * 0.00000986923; // 1 atmosphere = 101325 Pascals
    }

    public static double AtmospheresToPascals(double atmospheres)
    {
        return atmospheres / 0.00000986923; // 1 atmosphere = 101325 Pascals
    }

    public static double PascalsToBars(double pascals)
    {
        return pascals * 0.00001; // 1 bar = 100000 Pascals
    }

    public static double BarsToPascals(double bars)
    {
        return bars * 100000; // 1 bar = 100000 Pascals
    }

    public static double PascalsToMillibars(double pascals)
    {
        return pascals * 0.01; // 1 millibar = 100 Pascals
    }

    public static double MillibarsToPascals(double millibars)
    {
        return millibars * 100; // 1 millibar = 100 Pascals
    }

    public static double PascalsToTorr(double pascals)
    {
        return pascals * 0.00750062; // 1 Torr = 133.322 Pascals
    }

    public static double TorrToPascals(double torr)
    {
        return torr * 133.322; // 1 Torr = 133.322 Pascals
    }
}