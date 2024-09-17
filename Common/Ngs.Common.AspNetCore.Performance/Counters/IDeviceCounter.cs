namespace Ngs.Common.AspNetCore.Performance.Counters;

public interface IDeviceCounter : IDisposable
{
    public ulong TotalMemory { get; }
    
    public float GetCpuUsage();
    public float GetCpuUsage(int index);
    
    public float GetMemoryUsage();
    public float GetMemoryPercentageUsage();
    
    public float GetNetworkUsage();
    public float GetNetworkUsage(string network);
    public float GetNetworkUsage(int index);
    
    public float GetNetworkDownload();
    public float GetNetworkDownload(string network);
    public float GetNetworkDownload(int index);
    
    public float GetNetworkUpload();
    public float GetNetworkUpload(string network);
    public float GetNetworkUpload(int index);
    
    public float GetDiskUsage();
    public float GetDiskUsage(string disk);
    public float GetDiskUsage(int index);
}