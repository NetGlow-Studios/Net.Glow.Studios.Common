using System.Runtime.InteropServices;

namespace Ngs.Common.AspNetCore.Performance.Process;

public class ProcessUsage : IProcessUsage
{
    public string ProcessName { get; }
    public System.Diagnostics.Process Process { get; set; }
    public float TotalMemory { get; }
    
    private readonly IProcessUsage _processUsage;

    public ProcessUsage(System.Diagnostics.Process process)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            Process = process;
            _processUsage = new WindowsProcessUsage(process);
            TotalMemory = _processUsage.TotalMemory;
            ProcessName = _processUsage.ProcessName;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            Process = process;
            _processUsage = new LinuxProcessUsage();
            TotalMemory = _processUsage.TotalMemory;
            ProcessName = _processUsage.ProcessName;
        }
        else
        {
            throw new PlatformNotSupportedException("This platform is not supported for process usage. Supported platforms are Windows and Linux.");
        }
    }

    public ProcessUsage(string processName)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _processUsage = new WindowsProcessUsage(processName);
            TotalMemory = _processUsage.TotalMemory;
            ProcessName = _processUsage.ProcessName;
            Process = _processUsage.Process;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            _processUsage = new LinuxProcessUsage();
            TotalMemory = _processUsage.TotalMemory;
            ProcessName = _processUsage.ProcessName;
            Process = _processUsage.Process;
        }
        else
        {
            throw new PlatformNotSupportedException("This platform is not supported for process usage. Supported platforms are Windows and Linux.");
        }
    }
    
    public double GetCpuUsage() => _processUsage.GetCpuUsage();

    public double GetMemoryUsage() => _processUsage.GetMemoryUsage();

    public double GetMemoryPercentageUsage() => _processUsage.GetMemoryPercentageUsage();
    public double GetNetworkUsage() => _processUsage.GetNetworkUsage();

    public double GetNetworkDownload() => _processUsage.GetNetworkDownload();

    public double GetNetworkUpload() => _processUsage.GetNetworkUpload();

    public double GetDiskUsage() => _processUsage.GetDiskUsage();

    public void Dispose()
    {
        _processUsage.Dispose();
        GC.SuppressFinalize(this);
    }
}