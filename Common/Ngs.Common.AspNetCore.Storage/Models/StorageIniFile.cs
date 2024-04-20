namespace Ngs.Common.AspNetCore.Storage.Models;

public class StorageIniFile : StorageFile
{
    private Dictionary<string, string> Values { get; } = new();


    public StorageIniFile(FileSystemInfo file, StorageItem parent) : base(file, parent)
    {
        ContentType = "text/plain";

        var lines = File.ReadAllLines(FullPath);

        foreach (var line in lines)
        {
            if (line.StartsWith('#') || string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var parts = line.Split("=");

            if (parts.Length != 2)
            {
                throw new InvalidDataException("The INI file is not valid.");
            }

            Values.Add(parts[0], parts[1]);
        }
    }

    /// <summary>
    /// Get the value of a key in the INI file.
    /// </summary>
    /// <param name="key"> The key to get the value of. </param>
    /// <returns> The value of the key. </returns>
    /// <exception cref="KeyNotFoundException"> Thrown when the key is not found in the INI file. </exception>
    public string GetValue(string key)
    {
        if (!Values.TryGetValue(key, out var value))
        {
            throw new KeyNotFoundException($"The key '{key}' was not found in the INI file.");
        }

        return value;
    }
    
    /// <summary>
    /// Set the value of a key in the INI file.
    /// </summary>
    /// <param name="key"> The key to set the value of. </param>
    /// <param name="value"> The value to set. </param>
    /// <exception cref="ArgumentNullException"> Thrown when the key is null or empty. </exception>
    /// <exception cref="KeyNotFoundException"> Thrown when the key is not found in the INI file. </exception>
    public void SetValue(string key, string value)
    {
        if(string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key), "The key cannot be null or empty.");
        }

        if (!Values.TryGetValue(key, out _))
        {
            throw new KeyNotFoundException($"The key '{key}' was not found in the INI file.");
        }
        
        if(Values[key].Equals(value, StringComparison.CurrentCultureIgnoreCase))
        {
            return;
        }
        
        Values[key] = value;

        var lines = File.ReadAllLines(FullPath).ToList();

        for (var i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith('#') || string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            var parts = lines[i].Split("=");

            if (!parts[0].Equals(key, StringComparison.CurrentCultureIgnoreCase)) continue;
                
            lines[i] = $"{key}={value}";
            break;
        }
        
        File.WriteAllLines(FullPath, lines);
    }
    
    /// <summary>
    /// Add a key and value to the INI file.
    /// </summary>
    /// <param name="key"> The key to add. </param>
    /// <param name="value"> The value to add. </param>
    /// <exception cref="ArgumentNullException"> Thrown when the key is null or empty. </exception>
    /// <exception cref="ArgumentException"> Thrown when the key already exists in the INI file. </exception>
    public void AddValue(string key, string value)
    {
        if(string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key), "The key cannot be null or empty.");
        }

        if (!Values.TryAdd(key, value))
        {
            throw new ArgumentException($"The key '{key}' already exists in the INI file.");
        }

        var lines = File.ReadAllLines(FullPath).ToList();

        lines.Add($"{key}={value}");

        File.WriteAllLines(FullPath, lines);
    }
    
    /// <summary>
    /// Remove a key and value from the INI file.
    /// </summary>
    /// <param name="key"> The key to remove. </param>
    /// <exception cref="ArgumentNullException"> Thrown when the key is null or empty. </exception>
    /// <exception cref="KeyNotFoundException"> Thrown when the key is not found in the INI file. </exception>
    public void RemoveValue(string key)
    {
        if(string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException(nameof(key), "The key cannot be null or empty.");
        }

        if (!Values.Remove(key))
        {
            throw new KeyNotFoundException($"The key '{key}' was not found in the INI file.");
        }

        var lines = File.ReadAllLines(FullPath).ToList();

        for (var i = 0; i < lines.Count; i++)
        {
            if (lines[i].StartsWith('#') || string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }

            var parts = lines[i].Split("=");

            if (!parts[0].Equals(key, StringComparison.CurrentCultureIgnoreCase)) continue;
                
            lines.RemoveAt(i);
            break;
        }
        
        File.WriteAllLines(FullPath, lines);
    }
}