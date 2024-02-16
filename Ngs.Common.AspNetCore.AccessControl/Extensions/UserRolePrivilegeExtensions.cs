using Ngs.Common.AspNetCore.AccessControl.Enums;
using Ngs.Common.AspNetCore.AccessControl.Exceptions;

namespace Ngs.Common.AspNetCore.AccessControl.Extensions;

/// <summary>
/// Extensions for user role privilege.
/// </summary>
public static class UserRolePrivilegeExtensions
{
    /// <summary>
    /// Get privilege from string.
    /// </summary>
    /// <param name="str"> String to get privilege from. </param>
    /// <param name="flag"> Flag to get privilege for. </param>
    /// <returns> User privilege state. </returns>
    /// <exception cref="StringCannotBeFlagException"> Thrown when string cannot be flag. </exception>
    public static UserPrivilegeStateEnum GetPrivilege(this string str, Enum flag)
    {
        if (str.Any(x => x != '0' && x != '1'))
        {
            throw new StringCannotBeFlagException($"Current string cannot be flag <{flag.ToString()}>: '{str}'");
        }
        
        return (UserPrivilegeStateEnum)str.Reverse().ToArray()[Convert.ToInt32(flag)] - '0';
    }
    
    /// <summary>
    /// Check if string has privilege.
    /// </summary>
    /// <param name="str"> String to check privilege for. </param>
    /// <param name="flag"> Flag to check privilege for. </param>
    /// <param name="state"> State to check privilege for. </param>
    /// <returns> True if string has privilege, false otherwise. </returns>
    /// <exception cref="StringCannotBeFlagException"> Thrown when string cannot be flag. </exception>
    public static bool HasPrivilege(this string str, Enum flag, UserPrivilegeStateEnum state = UserPrivilegeStateEnum.Granted)
    {
        if (str.Any(x => x != '0' && x != '1'))
        {
            throw new StringCannotBeFlagException($"Current string cannot be flag <{flag.ToString()}>: '{str}'");
        }
        
        return str.Reverse().ToArray()[Convert.ToInt32(flag)] == ((int)state).ToString()[0];
    }

    /// <summary>
    /// Set privilege for string.
    /// </summary>
    /// <param name="str"> String to set privilege for. </param>
    /// <param name="flag"> Flag to set privilege for. </param>
    /// <param name="state"> State to set privilege for. </param>
    /// <returns> String with set privilege. </returns>
    /// <exception cref="ArgumentOutOfRangeException"> Thrown when flag index out of range. </exception>
    public static string SetPrivilege(this string str, Enum flag, UserPrivilegeStateEnum state)
    {
        var indexFromEnd = Convert.ToInt32(flag);
        var charArray = str.ToCharArray();
        
        if (indexFromEnd < 0 || indexFromEnd >= charArray.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(flag),"Flag index out of range");
        }
        
        charArray[charArray.Length - 1 - indexFromEnd] = ((int)state).ToString()[0];
        
        return new string(charArray);
    }
}