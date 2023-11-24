namespace Ngs.Common.Extensions.String;

public static class StringExtensions
{
    public static string PrintL(this string str)
    {
        Console.WriteLine(str);
        return str;
    }
    
    public static string Print(this string str)
    {
        Console.Write(str);
        return str;
    }

    public static string[] SplitByCapitalLetter(this string str)
    {
        return Array.Empty<string>();
    }
}