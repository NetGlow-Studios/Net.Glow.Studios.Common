namespace Ngs.Common.Tools.Color.Tests;

public class ColorConversionTests
{
    private string Hex { get; } = "451A54";
    private (int red, int green, int blue) Rgb { get; } = (69, 26, 84);
    private (double hue, double saturation, double value) Hsv { get; } = (284.48275862068965, 0.6904761904761905, 0.32941176470588235);
    private (double cyan, double magenta, double yellow, double black) Cmyk { get; } = (0.17857142857142863,0.6904761904761905, 0.00,0.6705882352941177);
    
    [Fact]
    public void HexToRgbTest()
    {
        var conversion = ColorConverter.HexToRgb(Hex);
        
        Assert.Equal(Rgb, conversion);
    }
    
    [Fact]
    public void RgbToHexTest()
    {
        var conversion = ColorConverter.RgbToHex(Rgb.red, Rgb.green, Rgb.blue);
        
        Assert.Equal(Hex, conversion);
    }
    
    [Fact]
    public void HsvToRgbTest()
    {
        var conversion = ColorConverter.HsvToRgb(Hsv.hue, Hsv.saturation, Hsv.value);
        
        Assert.Equal(Rgb, conversion);
    }
    
    [Fact]
    public void RgbToHsvTest()
    {
        var conversion = ColorConverter.RgbToHsv(Rgb.red, Rgb.green, Rgb.blue);
        
        Assert.Equal(Hsv, conversion);
    }
    
    [Fact]
    public void CmykToRgbTest()
    {
        var conversion = ColorConverter.CmykToRgb(Cmyk.cyan, Cmyk.magenta, Cmyk.yellow, Cmyk.black);

        Assert.Equal(Rgb, conversion);
    }
    
    [Fact]
    public void RgbToCmykTest()
    {
        var conversion = ColorConverter.RgbToCmyk(Rgb.red, Rgb.green, Rgb.blue);
        
        Assert.Equal(Cmyk, conversion);
    }

    [Fact]
    public void HexToHsvTest()
    {
        var conversion = ColorConverter.HexToHsv(Hex);
        
        Assert.Equal(Hsv, conversion);
    }
    
    [Fact]
    public void HsvToHexTest()
    {
        var conversion = ColorConverter.HsvToHex(Hsv.hue, Hsv.saturation, Hsv.value);
        
        Assert.Equal(Hex, conversion);
    }
    
    [Fact]
    public void HexToCmykTest()
    {
        var conversion = ColorConverter.HexToCmyk(Hex);
        
        Assert.Equal(Cmyk, conversion);
    }
    
    [Fact]
    public void CmykToHexTest()
    {
        var conversion = ColorConverter.CmykToHex(Cmyk.cyan, Cmyk.magenta, Cmyk.yellow, Cmyk.black);
        
        Assert.Equal(Hex, conversion);
    }
    
    [Fact]
    public void CmykToHsvTest()
    {
        var conversion = ColorConverter.CmykToHsv(Cmyk.cyan, Cmyk.magenta, Cmyk.yellow, Cmyk.black);
        
        Assert.Equal(Hsv, conversion);
    }

    [Fact]
    public void HsvToCmykTest()
    {
        var conversion = ColorConverter.HsvToCmyk(Hsv.hue, Hsv.saturation, Hsv.value);
        
        Assert.Equal(Cmyk, conversion);
    }
}