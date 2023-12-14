namespace Ngs.Common.Tools.Conversion;

public static class NumberConverter
{
    public static string DecimalToBin(decimal value)
    {
        return Convert.ToInt32(value).ToString();
    }
}