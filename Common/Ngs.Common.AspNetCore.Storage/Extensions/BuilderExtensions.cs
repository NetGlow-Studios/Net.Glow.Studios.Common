using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace Ngs.Common.AspNetCore.Storage.Extensions;

public static class BuilderExtensions
{
    public static IApplicationBuilder UseContentFileProvider(this IApplicationBuilder applicationBuilder, string path, string requestPath, HtmlDirectoryFormatter? formatter = default)
    {
        applicationBuilder.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider = new PhysicalFileProvider(path),
            RequestPath = requestPath,
            Formatter = formatter ?? new HtmlDirectoryFormatter(HtmlEncoder.Default)
        });

        return applicationBuilder;
    }
    
    public static IApplicationBuilder UseContentFileProvider(this IApplicationBuilder applicationBuilder, DirectoryBrowserOptions directoryBrowserOptions)
    {
        applicationBuilder.UseDirectoryBrowser(directoryBrowserOptions);
        return applicationBuilder;
    }
}