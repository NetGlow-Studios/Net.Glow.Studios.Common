using System.Net;
using Microsoft.AspNetCore.Mvc;
using Ngs.Common.AspNetCore.FluentFlow.Enums;

namespace Ngs.Common.AspNetCore.FluentFlow.Resp;

public class DownloadFileFluentResponse : BaseResponse
{
    public bool IsPhysicalFile { get; set; }
    public string? FilePath { get; set; }
    public byte[] FileContent { get; set; } = Array.Empty<byte>();
    public string FileName { get; set; } = string.Empty;

    public string ContentType { get; set; } = "application/octet-stream";

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