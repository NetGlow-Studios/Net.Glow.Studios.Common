using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.FluentFlow.Enums;

namespace Ngs.Common.AspNetCore.FluentFlow.Resp;

public class DownloadFileFluentResponse : BaseResponse
{
    /// <summary>
    /// If the file is a physical file or a byte array.
    /// </summary>
    public bool IsPhysicalFile { get; set; }
    
    /// <summary>
    /// Path to the file.
    /// </summary>
    public string? FilePath { get; set; }
    
    /// <summary>
    /// Content of the file.
    /// </summary>
    public byte[] FileContent { get; set; } = [];
    
    /// <summary>
    /// Name of the file.
    /// </summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Content type of the file.
    /// </summary>
    public string ContentType { get; set; } = "application/octet-stream";

    /// <summary>
    /// Returns an ActionResult based on the fluentResponse.
    /// </summary>
    /// <returns></returns>
    public override ActionResult GetActionResult()
    {
        StatusCode = HttpStatusCode.OK;
        RequiredAction = ResponseActionEnum.DownloadFile;
        
        if (IsPhysicalFile)
        {
            return new JsonResult(new
            {
                StatusCode = (int)StatusCode,
                RequiredAction,
                Content = new
                {
                    File = File.ReadAllBytes(FilePath!),
                    FileName,
                    ContentType
                }
            });
        }
        
        return new JsonResult(new
        {
            StatusCode = (int)StatusCode,
            RequiredAction,
            Content = new
            {
                File = FileContent,
                FileName,
                ContentType
            }
        });
    }
}