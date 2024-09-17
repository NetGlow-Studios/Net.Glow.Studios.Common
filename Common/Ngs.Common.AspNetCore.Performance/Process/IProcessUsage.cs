namespace Ngs.Common.AspNetCore.Performance.Process;

public interface IProcessUsage : IDisposable
{
    public string ProcessName { get; }
    public System.Diagnostics.Process Process { get; }
    
    public float TotalMemory { get; }
    
    public double GetCpuUsage();
    public double GetMemoryUsage();
    public double GetMemoryPercentageUsage();
    public double GetNetworkUsage();
    public double GetNetworkDownload();
    public double GetNetworkUpload();
    public double GetDiskUsage();
}