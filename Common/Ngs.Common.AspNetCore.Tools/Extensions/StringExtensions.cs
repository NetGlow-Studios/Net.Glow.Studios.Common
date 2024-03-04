using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Ngs.Common.AspNetCore.Tools.Extensions;

/// <summary>
/// Defines extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Removes invalid characters for file name.
    /// </summary>
    /// <param name="str"> The string to remove invalid characters from. </param>
    /// <returns> The string without invalid characters for file name. </returns>
    public static string RemoveInvalidCharsForFileName(this string str)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var regexPattern = $"[{Regex.Escape(new string(invalidChars))}]";
        return Regex.Replace(str, regexPattern, "");
    }
    
    /// <summary>
    /// Removes all special characters without dot and space.
    /// </summary>
    /// <param name="str"> The string to remove special characters from. </param>
    /// <returns> The string without special characters. </returns>
    public static string RemoveSpecialChars(this string str)
    {
        return Regex.Replace(str, @"[^a-zA-Z0-9\s\-.]", "", RegexOptions.Compiled);
    }
    
    /// <summary>
    /// Deserializes the JSON string to the specified type.
    /// </summary>
    /// <param name="json"> The JSON string to deserialize. </param>
    /// <typeparam name="T"> The type to deserialize the JSON string to. </typeparam>
    /// <returns> The deserialized object. </returns>
    public static T? JsonToObject<T>(this string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
    
    /// <summary>
    /// Replaces the character at the specified index with the new character.
    /// </summary>
    /// <param name="str"> The string to replace the character in. </param>
    /// <param name="index"> The index of the character to replace. </param>
    /// <param name="newChar"> The new character to replace the old character with. </param>
    /// <returns> The string with the character replaced. </returns>
    /// <exception cref="IndexOutOfRangeException"> Thrown when the index is out of range. </exception>
    public static string ReplaceCharAtIndex(this string str, int index, char newChar)
    {
        if (index < 0 || index >= str.Length)
        {
            throw new IndexOutOfRangeException("Index is out of range.");
        }

        var charArray = str.ToCharArray();
        charArray[index] = newChar;
        return new string(charArray);
    }
}