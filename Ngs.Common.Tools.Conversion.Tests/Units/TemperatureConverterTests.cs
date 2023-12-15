using Ngs.Common.Tools.Conversion.Units;

namespace Ngs.Common.Tools.Conversion.Tests.Units;

public class TemperatureConverterTests
{
    [Fact]
    public void CelsiusToFahrenheitTest()
    {
        const double celsius = 45.5;
        const double expected = 113.9;

        var convertedTemperature = TemperatureConverter.CelsiusToFahrenheit(celsius);
        
        Assert.Equal(expected, convertedTemperature);
    }

    [Fact]
    public void CelsiusToKelvinTest()
    {
        const double celsius = 45.5;
        const double expected = 318.65;

        var convertedTemperature = TemperatureConverter.CelsiusToKelvin(celsius);
        
        Assert.Equal(expected, convertedTemperature);
    }

    
    [Fact]
    public void FahrenheitToCelsius()
    {
        const double fahrenheit = 113.9;
        const double expected = 45.5;

        var convertedTemperature = TemperatureConverter.FahrenheitToCelsius(fahrenheit);
        
        Assert.Equal(expected, convertedTemperature);
    }
    
    [Fact]
    public void FahrenheitToKelvin()
    {
        const double fahrenheit = 113.9;
        const double expected = 318.65;

        var convertedTemperature = TemperatureConverter.FahrenheitToKelvin(fahrenheit);
        
        Assert.Equal(expected, convertedTemperature);
    }
    
    [Fact]
    public void KelvinToFahrenheit()
    {
        const double kelvin = 318.65;
        const double expected = 113.9;

        var convertedTemperature = TemperatureConverter.KelvinToFahrenheit(kelvin);
        
        Assert.Equal(expected, convertedTemperature);
    }
    
    [Fact]
    public void KelvinToCelsius()
    {
        const double kelvin = 318.65;
        const double expected = 45.5;

        var convertedTemperature = TemperatureConverter.KelvinToCelsius(kelvin);
        
        Assert.Equal(expected, convertedTemperature);
    }
}