namespace Ngs.Common.Tools.Conversion.Physics;

public class ElectricityConverter
{
    /// <summary>
    /// Converts Watts to Amperes.
    /// </summary>
    /// <param name="watts"> Power in Watts. </param>
    /// <param name="voltage"> Voltage in Volts. </param>
    /// <returns> Current in Amperes. </returns>
    public static double WattsToAmperes(double watts, double voltage)
    {
        // Power (Watts) = Voltage (Volts) * Current (Amperes)
        return watts / voltage; // Amperes = Watts / Volts
    }

    /// <summary>
    /// Converts Amperes to Watts.
    /// </summary>
    /// <param name="amperes"> Current in Amperes. </param>
    /// <param name="voltage"> Voltage in Volts. </param>
    /// <returns></returns>
    public static double AmperesToWatts(double amperes, double voltage)
    {
        // Power (Watts) = Voltage (Volts) * Current (Amperes)
        return amperes * voltage; // Watts = Volts * Amperes
    }

    /// <summary>
    /// Converts Volts to Watts.
    /// </summary>
    /// <param name="volts"> Voltage in Volts. </param>
    /// <param name="amperes"> Current in Amperes. </param>
    /// <returns> Power in Watts. </returns>
    public static double VoltsToWatts(double volts, double amperes)
    {
        // Power (Watts) = Voltage (Volts) * Current (Amperes)
        return volts * amperes; // Watts = Volts * Amperes
    }

    /// <summary>
    /// Converts Watts to Volts.
    /// </summary>
    /// <param name="watts"> Power in Watts. </param>
    /// <param name="amperes"> Current in Amperes. </param>
    /// <returns> Voltage in Volts. </returns>
    public static double WattsToVolts(double watts, double amperes)
    {
        // Power (Watts) = Voltage (Volts) * Current (Amperes)
        return watts / amperes; // Volts = Watts / Amperes
    }

    /// <summary>
    /// Converts Resistance to Voltage.
    /// </summary>
    /// <param name="resistance"> Resistance in Ohms. </param>
    /// <param name="current"> Current in Amperes. </param>
    /// <returns> Voltage in Volts. </returns>
    public static double VoltageFromResistanceAndCurrent(double resistance, double current)
    {
        // Ohm's Law: Voltage (Volts) = Resistance (Ohms) * Current (Amperes)
        return resistance * current; // Volts = Ohms * Amperes
    }

    /// <summary>
    /// Converts Voltage to Resistance.
    /// </summary>
    /// <param name="voltage"> Voltage in Volts. </param>
    /// <param name="current"> Current in Amperes. </param>
    /// <returns> Resistance in Ohms. </returns>
    public static double ResistanceFromVoltageAndCurrent(double voltage, double current)
    {
        // Ohm's Law: Resistance (Ohms) = Voltage (Volts) / Current (Amperes)
        return voltage / current; // Ohms = Volts / Amperes
    }

    /// <summary>
    /// Converts Voltage to Current.
    /// </summary>
    /// <param name="voltage"> Voltage in Volts. </param>
    /// <param name="resistance"> Resistance in Ohms. </param>
    /// <returns> Current in Amperes. </returns>
    public static double CurrentFromVoltageAndResistance(double voltage, double resistance)
    {
        // Ohm's Law: Current (Amperes) = Voltage (Volts) / Resistance (Ohms)
        return voltage / resistance; // Amperes = Volts / Ohms
    }
    
    /// <summary>
    /// Converts Volts to Millivolts.
    /// </summary>
    /// <param name="volts"> Voltage in Volts. </param>
    /// <returns> Voltage in Millivolts. </returns>
    public static double VoltsToMillivolts(double volts)
    {
        return volts * 1000; // 1 volt = 1000 millivolts
    }

    /// <summary>
    /// Converts Millivolts to Volts.
    /// </summary>
    /// <param name="millivolts"> Voltage in Millivolts. </param>
    /// <returns> Voltage in Volts. </returns>
    public static double MillivoltsToVolts(double millivolts)
    {
        return millivolts / 1000; // 1 volt = 1000 millivolts
    }
}