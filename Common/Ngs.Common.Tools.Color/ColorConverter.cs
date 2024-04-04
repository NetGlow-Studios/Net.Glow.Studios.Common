namespace Ngs.Common.Tools.Color;

/// <summary>
/// 
/// </summary>
public static class ColorConverter
{
    /// <summary>
    /// Rgb to Hex conversion.
    /// </summary>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <param name="alpha"></param>
    /// <returns> Hex color. </returns>
    public static string RgbToHex(int red, int green, int blue, int alpha = 255)
    {
        var hex = string.Empty;

        hex += red.ToString("X2");
        hex += green.ToString("X2");
        hex += blue.ToString("X2");

        return hex + (alpha != 255 ? alpha.ToString("X2") : string.Empty);
    }

    /// <summary>
    /// Hex to Rgb conversion.
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static (int red, int green, int blue) HexToRgb(string hex)
    {
        if (hex.StartsWith("#")) hex = hex[1..]; // Remove '#' if present

        var red = Convert.ToInt32(hex[..2], 16);
        var green = Convert.ToInt32(hex.Substring(2, 2), 16);
        var blue = Convert.ToInt32(hex.Substring(4, 2), 16);

        return new ValueTuple<int, int, int>(red, green, blue);
    }

    /// <summary>
    /// Rgb to Cmyk conversion.
    /// </summary>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <returns></returns>
    public static (double cyan, double magenta, double yellow, double black) RgbToCmyk(int red, int green, int blue)
    {
        var c = 1 - (double)red / 255;
        var m = 1 - (double)green / 255;
        var y = 1 - (double)blue / 255;
        var k = Math.Min(c, Math.Min(m, y));

        if (Math.Abs(k - 1) < 0.01) // If all are 0, avoid division by 0
            return new ValueTuple<double, double, double, double>(0, 0, 0, 1);

        var cyan = (c - k) / (1 - k);
        var magenta = (m - k) / (1 - k);
        var yellow = (y - k) / (1 - k);

        return new ValueTuple<double, double, double, double>(cyan, magenta, yellow, k);
    }

    /// <summary>
    /// Cmyk to Rgb conversion.
    /// </summary>
    /// <param name="cyan"></param>
    /// <param name="magenta"></param>
    /// <param name="yellow"></param>
    /// <param name="black"></param>
    /// <returns></returns>
    public static (int red, int green, int blue) CmykToRgb(double cyan, double magenta, double yellow, double black)
    {
        var red = (int)Math.Round(255 * (1 - cyan) * (1 - black));
        var green = (int)Math.Round(255 * (1 - magenta) * (1 - black));
        var blue = (int)Math.Round(255 * (1 - yellow) * (1 - black));

        return new ValueTuple<int, int, int>(red, green, blue);
    }

    /// <summary>
    /// Rgb to Hsv conversion.
    /// </summary>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    /// <returns></returns>
    public static (double hue, double saturation, double value) RgbToHsv(int red, int green, int blue)
    {
        var r = (double)red / 255;
        var g = (double)green / 255;
        var b = (double)blue / 255;

        var max = Math.Max(r, Math.Max(g, b));
        var min = Math.Min(r, Math.Min(g, b));

        double hue = 0;

        if (Math.Abs(max - min) < 0.01)
        {
            hue = 0;
        }
        else if (Math.Abs(max - r) < 0.01)
        {
            hue = 60 * ((g - b) / (max - min)) % 360;
        }
        else if (Math.Abs(max - g) < 0.01)
        {
            hue = 60 * ((b - r) / (max - min)) + 120;
        }
        else if (Math.Abs(max - b) < 0.01)
        {
            hue = 60 * ((r - g) / (max - min)) + 240;
        }

        if (hue < 0)
        {
            hue += 360;
        }

        var saturation = (max == 0) ? 0 : 1 - (min / max);

        return new ValueTuple<double, double, double>(hue, saturation, max);
    }

    /// <summary>
    /// Hsv to Rgb conversion.
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (int red, int green, int blue) HsvToRgb(double hue, double saturation, double value)
    {
        var hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        var f = hue / 60 - Math.Floor(hue / 60);
        var v = value * 255;
        var p = v * (1 - saturation);
        var q = v * (1 - f * saturation);
        var t = v * (1 - (1 - f) * saturation);

        var red = Convert.ToInt32(v);

        return hi switch
        {
            0 => (red, Convert.ToInt32(v), Convert.ToInt32(p)),
            1 => (Convert.ToInt32(q), red, Convert.ToInt32(p)),
            2 => (Convert.ToInt32(p), red, Convert.ToInt32(t)),
            3 => (Convert.ToInt32(p), Convert.ToInt32(q), red),
            4 => (Convert.ToInt32(t), Convert.ToInt32(p), red),
            _ => (red, Convert.ToInt32(p), Convert.ToInt32(q))
        };
    }

    /// <summary>
    /// Hex to Hsv conversion.
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static (double hue, double saturation, double value) HexToHsv(string hex)
    {
        var rgb = HexToRgb(hex);
        return RgbToHsv(rgb.red, rgb.green, rgb.blue);
    }

    /// <summary>
    /// Hsv to Hex conversion.
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string HsvToHex(double hue, double saturation, double value)
    {
        var rgb = HsvToRgb(hue, saturation, value);
        return RgbToHex(rgb.red, rgb.green, rgb.blue);
    }

    /// <summary>
    /// Hex to Cmyk conversion.
    /// </summary>
    /// <param name="hex"></param>
    /// <returns></returns>
    public static (double cyan, double magenta, double yellow, double black) HexToCmyk(string hex)
    {
        var rgb = HexToRgb(hex);
        return RgbToCmyk(rgb.red, rgb.green, rgb.blue);
    }

    /// <summary>
    /// Cmyk to Hex conversion.
    /// </summary>
    /// <param name="cyan"></param>
    /// <param name="magenta"></param>
    /// <param name="yellow"></param>
    /// <param name="black"></param>
    /// <returns></returns>
    public static string CmykToHex(double cyan, double magenta, double yellow, double black)
    {
        var rgb = CmykToRgb(cyan, magenta, yellow, black);
        return RgbToHex(rgb.red, rgb.green, rgb.blue);
    }

    /// <summary>
    /// Hsv to Cmyk conversion.
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static (double cyan, double magenta, double yellow, double black) HsvToCmyk(double hue, double saturation,
        double value)
    {
        var rgb = HsvToRgb(hue, saturation, value);
        return RgbToCmyk(rgb.red, rgb.green, rgb.blue);
    }

    /// <summary>
    /// Cmyk to Hsv conversion.
    /// </summary>
    /// <param name="cyan"></param>
    /// <param name="magenta"></param>
    /// <param name="yellow"></param>
    /// <param name="black"></param>
    /// <returns></returns>
    public static (double hue, double saturation, double value) CmykToHsv(double cyan, double magenta, double yellow,
        double black)
    {
        var rgb = CmykToRgb(cyan, magenta, yellow, black);
        return RgbToHsv(rgb.red, rgb.green, rgb.blue);
    }
}