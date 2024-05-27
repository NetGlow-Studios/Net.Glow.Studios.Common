using MessagePack;

namespace Ngs.Common.AspNetCore.Storage.Backup;

public class PackData<T> where T : class
{
    public void CreateBackup(T data, string filePath)
    {
        try
        {
            var bytes = MessagePackSerializer.Serialize(data);
            
            File.WriteAllBytes(filePath, bytes);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error creating backup: " + ex.Message);
        }
    }
    
    public T RestoreBackup(string filePath)
    {
        try
        {
            var bytes = File.ReadAllBytes(filePath);
            var data = MessagePackSerializer.Deserialize<T>(bytes);
            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error restoring backup: " + ex.Message);
            return default(T)!;
        }
    }
}