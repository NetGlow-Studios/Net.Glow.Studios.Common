using SharpCompress.Archives;
using SharpCompress.Common;

namespace Ngs.Common.File.Compressions;

public static class WinRarCompressor
{
    public static Task Extract(string archivePath, string destinationFolder)
    {
        using var archive = SharpCompress.Archives.ArchiveFactory.Open(archivePath);

        foreach (var entry in archive.Entries)
        {
            if (!entry.IsDirectory)
            {
                entry.WriteToDirectory(destinationFolder, new ExtractionOptions()
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }
        }

        return Task.CompletedTask;
    }
}