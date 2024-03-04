using Ngs.Common.AspNetCore.FluentFlow.Resp;

namespace Ngs.Common.AspNetCore.FluentFlow.Extensions;

/// <summary>
/// Extension methods for <see cref="DownloadFileFluentResponse"/>.
/// </summary>
public static class DownloadResponseExtensions
{
    /// <summary>
    /// Returns a physical file from the given path.
    /// </summary>
    /// <param name="response"> The response. </param>
    /// <param name="filePath"> The file path. </param>
    /// <param name="fileName"> The file name. </param>
    /// <param name="contentType"> The content type. </param>
    /// <returns> The <see cref="DownloadFileFluentResponse"/>. </returns>
    public static DownloadFileFluentResponse ReturnPhysicalFile(this DownloadFileFluentResponse response, string filePath, string fileName, string contentType = "application/octet-stream")
    {
        response.IsPhysicalFile = true;
        response.FilePath = filePath;
        response.FileName = fileName;
        response.ContentType = contentType;
        return response;
    }
    
    /// <summary>
    /// Returns a file from the given content.
    /// </summary>
    /// <param name="response"> The response. </param>
    /// <param name="fileContent"> The file content. </param>
    /// <param name="fileName"> The file name. </param>
    /// <param name="contentType"> The content type. </param>
    /// <returns></returns>
    public static DownloadFileFluentResponse ReturnFile(this DownloadFileFluentResponse response, byte[] fileContent, string fileName, string contentType = "application/octet-stream")
    {
        response.FileContent = fileContent;
        response.FileName = fileName;
        response.ContentType = contentType;
        return response;
    }
}