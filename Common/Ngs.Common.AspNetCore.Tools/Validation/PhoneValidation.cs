using PhoneNumbers;

namespace Ngs.Common.AspNetCore.Tools.Validation;

/// <summary>
/// Phone number validation
/// </summary>
public static class PhoneValidation
{
    /// <summary>
    /// Check if phone number is valid
    /// </summary>
    /// <param name="phoneNumber"> Phone number </param>
    /// <returns> True if phone number is valid </returns>
    public static bool IsValid(string phoneNumber)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        
        try
        {
            var parsedPhoneNumber = phoneNumberUtil.Parse(phoneNumber, null);
            return phoneNumberUtil.IsValidNumber(parsedPhoneNumber);
        }
        catch (NumberParseException)
        {
            return false;
        }
    }
}