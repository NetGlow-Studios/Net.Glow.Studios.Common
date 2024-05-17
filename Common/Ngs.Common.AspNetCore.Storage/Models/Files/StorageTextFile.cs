namespace Ngs.Common.AspNetCore.Storage.Models.Files;

public class StorageTextFile : StorageFile
{
    public StorageTextFile(FileInfo file, StorageFolderItem parent) : base(file, parent)
    {
    }
    
    public string ReadAllText()
    {
        return File.ReadAllText(AbsolutePath);
    }
    
    public void WriteAllText(string text)
    {
        File.WriteAllText(AbsolutePath, text);
    }
    
    public void AppendText(string text)
    {
        File.AppendAllText(AbsolutePath, text);
    }
    
    public void AppendLine(string text)
    {
        File.AppendAllText(AbsolutePath, $"{text}{Environment.NewLine}");
    }
    
    public void WriteLines(IEnumerable<string> lines)
    {
        File.WriteAllLines(AbsolutePath, lines);
    }
    
    public IEnumerable<string> ReadLines()
    {
        return File.ReadAllLines(AbsolutePath);
    }
}