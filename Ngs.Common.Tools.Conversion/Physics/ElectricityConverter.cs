namespace Ngs.Common.Tools.Conversion.Physics;

public class ElectricityConverter
{
    public static double WattsToAmperes(double watts, double voltage)
    {
        // Power (Watts) = Voltage (Volts) * Current (Amperes)
        return watts / voltage; // Amperes = Watts / Volts
    }

    public static double AmperesToWatts(double amperes, double voltage)
    {
        // Power (Watts) = Voltage (Volts) * Current (Amperes)
        return amperes * voltage; // Watts = Volts * Amperes
    }

    public static double VoltsToWatts(double volts, double amperes)
    {
        // Power (Watts) = Voltage (Volts) * Current (Amperes)
        return volts * amperes; // Watts = Volts * Amperes
    }

    public static double WattsToVolts(double watts, double amperes)
    {
        // Power (Watts) = Voltage (Volts) * Current (Amperes)
        return watts / amperes; // Volts = Watts / Amperes
    }

    public static double VoltageFromResistanceAndCurrent(double resistance, double current)
    {
        // Ohm's Law: Voltage (Volts) = Resistance (Ohms) * Current (Amperes)
        return resistance * current; // Volts = Ohms * Amperes
    }

    public static double ResistanceFromVoltageAndCurrent(double voltage, double current)
    {
        // Ohm's Law: Resistance (Ohms) = Voltage (Volts) / Current (Amperes)
        return voltage / current; // Ohms = Volts / Amperes
    }

    public static double CurrentFromVoltageAndResistance(double voltage, double resistance)
    {
        // Ohm's Law: Current (Amperes) = Voltage (Volts) / Resistance (Ohms)
        return voltage / resistance; // Amperes = Volts / Ohms
    }
    
    public static double VoltsToMillivolts(double volts)
    {
        return volts * 1000; // 1 volt = 1000 millivolts
    }

    public static double MillivoltsToVolts(double millivolts)
    {
        return millivolts / 1000; // 1 volt = 1000 millivolts
    }
}