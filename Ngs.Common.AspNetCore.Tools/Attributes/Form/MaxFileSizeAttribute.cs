using Microsoft.AspNetCore.Mvc.Filters;
using Ngs.Common.AspNetCore.Tools.Enums.Form;
using Ngs.Common.AspNetCore.Tools.Exceptions;

namespace Ngs.Common.AspNetCore.Tools.Attributes.Form;

[AttributeUsage(AttributeTargets.Method)]
public class MaxFileSizeAttribute(long maxFileSize) : Attribute, IActionFilter
{
    public MaxFileSizeAttribute(MaxFileSizeEnum maxFileSize):this((long)maxFileSize)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var files = context.HttpContext.Request.Form.Files;
        foreach (var file in files)
        {
            if (file.Length <= maxFileSize) continue;
            
            throw new FileSizeLimitExceededException($"File: '{file.FileName}' exceeds the limit of '{maxFileSize}' bytes. Current file size: '{file.Length}' bytes.");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}