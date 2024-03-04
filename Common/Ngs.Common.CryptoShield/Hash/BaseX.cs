namespace Ngs.Common.CryptoShield.Hash;

public class BaseX(string key)
{
    public string Key { get; set; } = key;

    public static string Base64Encode(string value)
    {
        var valueBytes = System.Text.Encoding.UTF8.GetBytes(value);
        return System.Convert.ToBase64String(valueBytes);
    }
    
    public static string Base64Decode(string value) 
    {
        var valueBytes = System.Convert.FromBase64String(value);
        return System.Text.Encoding.UTF8.GetString(valueBytes);
    }
}