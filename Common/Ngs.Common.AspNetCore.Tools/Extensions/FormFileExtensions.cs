using Microsoft.AspNetCore.Http;

namespace Ngs.Common.AspNetCore.Tools.Extensions;

/// <summary>
/// Defines extension methods for <see cref="IFormFile"/>.
/// </summary>
public static class FormFileExtensions
{
    /// <summary>
    /// Reads asynchronously the content of the <see cref="IFormFile"/> as a byte array.
    /// </summary>
    /// <param name="formFile"> The <see cref="IFormFile"/> to read. </param>
    /// <returns> The content of the <see cref="IFormFile"/> as a byte array. </returns>
    public static async Task<byte[]> ToArrayAsync(this IFormFile formFile)
    {
        await using var stream = formFile.OpenReadStream();
        using var binaryReader = new BinaryReader(stream);
        
        return binaryReader.ReadBytes((int)stream.Length);
    }
    
    /// <summary>
    /// Reads the content of the <see cref="IFormFile"/> as a byte array.
    /// </summary>
    /// <param name="formFile"> The <see cref="IFormFile"/> to read. </param>
    /// <returns> The content of the <see cref="IFormFile"/> as a byte array. </returns>
    public static byte[] ToArray(this IFormFile formFile)
    {
        using var stream = formFile.OpenReadStream();
        using var binaryReader = new BinaryReader(stream);
        
        return binaryReader.ReadBytes((int)stream.Length);
    }

    /// <summary>
    /// Reads asynchronously the content of the <see cref="IFormFile"/> as a string.
    /// </summary>
    /// <param name="formFile"> The <see cref="IFormFile"/> to read. </param>
    /// <returns> The content of the <see cref="IFormFile"/> as a string. </returns>
    public static async Task<string> ToStringAsync(this IFormFile formFile)
    {
        using var streamReader = new StreamReader(formFile.OpenReadStream());

        return await streamReader.ReadToEndAsync();
    }
    
    /// <summary>
    /// Reads the content of the <see cref="IFormFile"/> as a string.
    /// </summary>
    /// <param name="formFile"> The <see cref="IFormFile"/> to read. </param>
    /// <returns> The content of the <see cref="IFormFile"/> as a string. </returns>
    public static string ToString(this IFormFile formFile)
    {
        using var streamReader = new StreamReader(formFile.OpenReadStream());

        return streamReader.ReadToEnd();
    }
}