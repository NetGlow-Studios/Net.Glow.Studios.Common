using Ngs.Common.Tools.Conversion.Units;

namespace Ngs.Common.Tools.Conversion.Tests.Units;

public class WeightConverterTests
{
    [Fact]
    public void PoundsToKilogramsTest()
    {
        const double pounds = 10;
        const double expected = 4.5359;

        var kilograms = WeightConverter.PoundsToKilograms(pounds);
        
        Assert.Equal(Math.Round(expected, 3), Math.Round(kilograms, 3));
    }
    
    [Fact]
    public void KilogramsToPoundsTest()
    {
        const double kilograms = 4.5359;
        const double expected = 10;

        var pounds = WeightConverter.KilogramsToPounds(kilograms);
        
        Assert.Equal(Math.Round(expected, 3), Math.Round(pounds, 3));
    }
    
    [Fact]
    public void OuncesToGramsTest()
    {
        const double ounces = 10;
        const double expected = 283.4952;

        var grams = WeightConverter.OuncesToGrams(ounces);
        
        Assert.Equal(Math.Round(expected, 3), Math.Round(grams, 3));
    }
    
    [Fact]
    public void GramsToOuncesTest()
    {
        const double grams = 283.4952;
        const double expected = 10;

        var ounces = WeightConverter.GramsToOunces(grams);
        
        Assert.Equal(Math.Round(expected, 3), Math.Round(ounces, 3));
    }
}