using Ngs.Common.Tools.Color;
using Xunit;

namespace Ngs.Common.File.Tests;

public class ColorConverterTests
{
    private string Hex { get; } = "451A54";
    private (int red, int green, int blue) Rgb { get; } = (69, 26, 84);
    private (double hue, double saturation, double value) Hsv { get; } = (284.48275862068965, 0.6904761904761905, 0.32941176470588235);
    private (double cyan, double magenta, double yellow, double black) Cmyk { get; } = (0.17857142857142863,0.6904761904761905, 0.00,0.6705882352941177);
    
    [Fact]
    public void HexToRgb()
    {
        var conversion = ColorConverter.HexToRgb(Hex);
        
        Assert.Equal(Rgb, conversion);
    }
    
    [Fact]
    public void RgbToHex()
    {
        var conversion = ColorConverter.RgbToHex(Rgb.red, Rgb.green, Rgb.blue);
        
        Assert.Equal(Hex, conversion);
    }
    
    [Fact]
    public void HsvToRgb()
    {
        var conversion = ColorConverter.HsvToRgb(Hsv.hue, Hsv.saturation, Hsv.value);
        
        Assert.Equal(Rgb, conversion);
    }
    
    [Fact]
    public void RgbToHsv()
    {
        var conversion = ColorConverter.RgbToHsv(Rgb.red, Rgb.green, Rgb.blue);
        
        Assert.Equal(Hsv, conversion);
    }
    
    [Fact]
    public void CmykToRgb()
    {
        var conversion = ColorConverter.CmykToRgb(Cmyk.cyan, Cmyk.magenta, Cmyk.yellow, Cmyk.black);

        Assert.Equal(Rgb, conversion);
    }
    
    [Fact]
    public void RgbToCmyk()
    {
        var conversion = ColorConverter.RgbToCmyk(Rgb.red, Rgb.green, Rgb.blue);
        
        Assert.Equal(Cmyk, conversion);
    }

    [Fact]
    public void HexToHsv()
    {
        var conversion = ColorConverter.HexToHsv(Hex);
        
        Assert.Equal(Hsv, conversion);
    }
    
    [Fact]
    public void HsvToHex()
    {
        var conversion = ColorConverter.HsvToHex(Hsv.hue, Hsv.saturation, Hsv.value);
        
        Assert.Equal(Hex, conversion);
    }
    
    [Fact]
    public void HexToCmyk()
    {
        var conversion = ColorConverter.HexToCmyk(Hex);
        
        Assert.Equal(Cmyk, conversion);
    }
    
    [Fact]
    public void CmykToHex()
    {
        var conversion = ColorConverter.CmykToHex(Cmyk.cyan, Cmyk.magenta, Cmyk.yellow, Cmyk.black);
        
        Assert.Equal(Hex, conversion);
    }
    
    [Fact]
    public void CmykToHsv()
    {
        var conversion = ColorConverter.CmykToHsv(Cmyk.cyan, Cmyk.magenta, Cmyk.yellow, Cmyk.black);
        
        Assert.Equal(Hsv, conversion);
    }

    [Fact]
    public void HsvToCmyk()
    {
        var conversion = ColorConverter.HsvToCmyk(Hsv.hue, Hsv.saturation, Hsv.value);
        
        Assert.Equal(Cmyk, conversion);
    }
}