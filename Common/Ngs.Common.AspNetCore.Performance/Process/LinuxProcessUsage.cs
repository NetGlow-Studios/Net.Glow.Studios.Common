namespace Ngs.Common.AspNetCore.Performance.Process;

public class LinuxProcessUsage : IProcessUsage
{
    public void Dispose()
    {
        
    }

    public string ProcessName { get; }
    public System.Diagnostics.Process Process { get; }
    public float TotalMemory { get; }
    public double GetCpuUsage()
    {
        throw new NotImplementedException();
    }

    public double GetMemoryUsage()
    {
        throw new NotImplementedException();
    }

    public double GetMemoryPercentageUsage()
    {
        throw new NotImplementedException();
    }

    public double GetNetworkUsage()
    {
        throw new NotImplementedException();
    }

    public double GetNetworkDownload()
    {
        throw new NotImplementedException();
    }

    public double GetNetworkUpload()
    {
        throw new NotImplementedException();
    }

    public double GetDiskUsage()
    {
        throw new NotImplementedException();
    }
}