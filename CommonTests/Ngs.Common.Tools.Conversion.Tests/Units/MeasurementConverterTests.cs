using Ngs.Common.Tools.Conversion.Units;

namespace Ngs.Common.Tools.Conversion.Tests.Units;

public class MeasurementConverterTests
{
    [Fact]
    public void InchesToCentimetersTest()
    {
        const double inches = 10;
        const double expected = 25.4;

        var centimeters = MeasurementConverter.InchesToCentimeters(inches);
        
        Assert.Equal(expected, centimeters);
    }
    
    [Fact]
    public void CentimetersToInchesTest()
    {
        const double centimeters = 25.4;
        const double expected = 10;

        var inches = MeasurementConverter.CentimetersToInches(centimeters);
        
        Assert.Equal(expected, inches);
    }
    
    [Fact]
    public void FeetToMetersTest()
    {
        const double feet = 10;
        const double expected = 3.04800;

        var meters = MeasurementConverter.FeetToMeters(feet);
        
        Assert.Equal(expected, meters);
    }
    
    [Fact]
    public void MetersToFeetTest()
    {
        const double meters = 3.04800;
        const double expected = 10;

        var feet = MeasurementConverter.MetersToFeet(meters);
        
        Assert.Equal(expected, feet);
    }
    
    [Fact]
    public void MilesToKilometersTest()
    {
        const double miles = 10;
        const double expected = 16.093399999999999;

        var kilometers = MeasurementConverter.MilesToKilometers(miles);
        
        Assert.Equal(expected, kilometers);
    }
    
    [Fact]
    public void KilometersToMilesTest()
    {
        const double kilometers = 16.093399999999999;
        const double expected = 10;

        var miles = MeasurementConverter.KilometersToMiles(kilometers);
        
        Assert.Equal(expected, miles);
    }
}