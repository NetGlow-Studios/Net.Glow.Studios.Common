namespace Ngs.Common.AspNetCore.Storage.Models.Files;

public sealed class StorageIniFile : StorageTextFile
{
    private Dictionary<string, string> Values { get; }
    
    public StorageIniFile(FileInfo file, StorageFolderItem parent) : base(file, parent)
    {
        Values = new Dictionary<string, string>();
        
        var lines = File.ReadAllLines(AbsolutePath);
        
        foreach (var line in lines)
        {
            if (line.StartsWith('#') || string.IsNullOrWhiteSpace(line)) continue;
            
            var parts = line.Split('=');
            
            if (parts.Length != 2) continue;
            
            Values.Add(parts[0], parts[1]);
        }
    }
    
    /// <summary>
    /// Get value by key
    /// </summary>
    /// <param name="key"> Key </param>
    /// <returns> Value </returns>
    public string GetValue(string key)
    {
        return Values.TryGetValue(key, out var value) ? value : string.Empty;
    }
    
    /// <summary>
    /// Set value by key
    /// </summary>
    /// <param name="key"> Key </param>
    /// <param name="value"> Value </param>
    public StorageIniFile SetValue(string key, string value)
    {
        Values[key] = value;
        
        return this;
    }
    
    /// <summary>
    /// Append element to the file
    /// </summary>
    /// <param name="key"> Key </param>
    /// <param name="value"> Value </param>
    /// <returns> Current Ini file </returns>
    public StorageIniFile AppendValue(string key, string value)
    {
        Values.TryAdd(key, value);

        return this;
    }
    
    /// <summary>
    /// Save the file
    /// </summary>
    /// <returns> Current Ini file </returns>
    public StorageIniFile Save()
    {
        var fileLines = File.ReadAllLines(AbsolutePath);
        
        for (var i = 0; i < fileLines.Length; i++)
        {
            if (fileLines[i].StartsWith('#') || string.IsNullOrWhiteSpace(fileLines[i])) continue;
            
            var parts = fileLines[i].Split('=');
            
            if (parts.Length != 2) continue;
            
            if (Values.TryGetValue(parts[0], out var value))
            {
                fileLines[i] = $"{parts[0]}={value}";
            }
        }
        
        File.WriteAllLines(AbsolutePath, fileLines);
        
        return this;
    }

    /// <summary>
    /// Save the file
    /// </summary>
    /// <returns> Current Ini file </returns>
    public async Task<StorageIniFile> SaveAsync()
    {
        var fileLines = await File.ReadAllLinesAsync(AbsolutePath);
        
        for (var i = 0; i < fileLines.Length; i++)
        {
            if (fileLines[i].StartsWith('#') || string.IsNullOrWhiteSpace(fileLines[i])) continue;
            
            var parts = fileLines[i].Split('=');
            
            if (parts.Length != 2) continue;
            
            if (Values.TryGetValue(parts[0], out var value))
            {
                fileLines[i] = $"{parts[0]}={value}";
            }
        }
        
        await File.WriteAllLinesAsync(AbsolutePath, fileLines);
        
        return this;
    }
}