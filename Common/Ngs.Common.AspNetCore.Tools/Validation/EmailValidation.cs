using System.Text.RegularExpressions;

namespace Ngs.Common.AspNetCore.Tools.Validation;

/// <summary>
/// Email validation
/// </summary>
public static class EmailValidation
{
    /// <summary>
    /// Check if email is valid
    /// </summary>
    /// <param name="email"> Email to validate </param>
    /// <returns> True if email is valid </returns>
    public static bool IsValid(string email)
    {
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        return Regex.IsMatch(email.Trim(), pattern);
    }
}