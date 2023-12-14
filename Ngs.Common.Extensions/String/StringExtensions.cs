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

    public static string[] Group(this string str, int count)
    {
        var result = new string[str.Length % count];

        for (var i = 0; i < str.Length % count; i++)
        {
            result[i] = str[i * count^(i+1)*count].ToString();
        }

        return result;
    }
}