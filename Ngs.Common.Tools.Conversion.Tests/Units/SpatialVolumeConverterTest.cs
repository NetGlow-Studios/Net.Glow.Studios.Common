using Ngs.Common.Tools.Conversion.Units;

namespace Ngs.Common.Tools.Conversion.Tests.Units;

public class SpatialVolumeConverterTest
{
    [Fact]
    public void LitersToGallons()
    {
        const double liters = 10;
        const double expected = 2.64172052;

        var gallons = SpatialVolumeConverter.LitersToGallons(liters);
        
        Assert.Equal(Math.Round(expected, 3), Math.Round(gallons, 3));
    }

    [Fact]
    public void GallonsToLiters()
    {
        const double gallons = 2.64172052;
        const double expected = 10;

        var liters = SpatialVolumeConverter.GallonsToLiters(gallons);
        
        Assert.Equal(Math.Round(expected, 3), Math.Round(liters, 3));
    }
}