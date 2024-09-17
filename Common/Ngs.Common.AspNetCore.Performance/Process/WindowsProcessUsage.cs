using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;


namespace Ngs.Common.AspNetCore.Performance.Process;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class WindowsProcessUsage : IProcessUsage
{
    public string ProcessName { get; }
    public System.Diagnostics.Process Process { get; }
    public float TotalMemory { get; private set; }

    private PerformanceCounter _cpuCounter;
    private PerformanceCounter _ramCounter;
    private PerformanceCounter[] _networkDownloadCounters;
    private PerformanceCounter[] _networkUploadCounters;
    private PerformanceCounter _diskCounter;

    public WindowsProcessUsage(System.Diagnostics.Process process)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException("This class is only supported on Windows.");
        }
        
        _cpuCounter = new PerformanceCounter();
        _ramCounter = new PerformanceCounter();
        _diskCounter = new PerformanceCounter();
        _networkDownloadCounters = [];
        _networkUploadCounters = [];

        Process = process;
        ProcessName = GetProcessInstanceName(Process.Id);
        
        Initialize(ProcessName);
    }

    public WindowsProcessUsage(string processName)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException("This class is only supported on Windows.");
        }
        
        _cpuCounter = new PerformanceCounter();
        _ramCounter = new PerformanceCounter();
        _diskCounter = new PerformanceCounter();
        _networkDownloadCounters = [];
        _networkUploadCounters = [];

        Process = GetProcessInstanceByName(processName);
        ProcessName = processName;
        
        Initialize(processName);
    }

    private void Initialize(string processName)
    {
        _cpuCounter = new PerformanceCounter("Process", "% Processor Time", processName, true);
        _ramCounter = new PerformanceCounter("Process", "Working Set - Private", processName, true);
        _diskCounter = new PerformanceCounter("Process", "IO Data Bytes/sec", processName, true);

        TotalMemory = GetTotalPhysicalMemory();

        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
            .Where(ni => ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                         ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                         ni.NetworkInterfaceType != NetworkInterfaceType.Unknown &&
                         !ni.Description.Contains("Virtual", StringComparison.OrdinalIgnoreCase) &&
                         !ni.Description.Contains("Hyper-V", StringComparison.OrdinalIgnoreCase) &&
                         !ni.Description.Contains("VMware", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        _networkDownloadCounters = new PerformanceCounter[networkInterfaces.Length];
        _networkUploadCounters = new PerformanceCounter[networkInterfaces.Length];

        for (var i = 0; i < networkInterfaces.Length; i++)
        {
            var networkName = networkInterfaces[i].Description;
            try
            {
                _networkDownloadCounters[i] =
                    new PerformanceCounter("Network Interface", "Bytes Received/sec", networkName);
                _networkUploadCounters[i] = new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkName);
            }
            catch (Exception ex)
            {
                // Handle exceptions for specific interfaces
                Console.WriteLine(
                    $"Failed to create PerformanceCounter for network interface {networkName}: {ex.Message}");
            }
        }
    }

    public double GetCpuUsage()
    {
        return _cpuCounter.NextValue();
    }

    public double GetMemoryUsage()
    {
        return _ramCounter.NextValue() / 1024;
    }

    public double GetMemoryPercentageUsage()
    {
        return GetMemoryUsage() / TotalMemory * 100;
    }

    public double GetNetworkUsage()
    {
        return _networkDownloadCounters.Sum(counter => counter.NextValue()) +
               _networkUploadCounters.Sum(counter => counter.NextValue());
    }

    public double GetNetworkDownload()
    {
        return _networkDownloadCounters.Sum(counter => counter.NextValue());
    }

    public double GetNetworkUpload()
    {
        return _networkUploadCounters.Sum(counter => counter.NextValue());
    }

    public double GetDiskUsage()
    {
        return _diskCounter.NextValue();
    }

    public void Dispose()
    {
        _cpuCounter.Dispose();
        _ramCounter.Dispose();
        _diskCounter.Dispose();

        foreach (var counter in _networkDownloadCounters)
        {
            counter.Dispose();
        }

        foreach (var counter in _networkUploadCounters)
        {
            counter.Dispose();
        }
    }

    private static string GetProcessInstanceName(int pid)
    {
        var cat = new PerformanceCounterCategory("Process");
        string[] instances = cat.GetInstanceNames();

        foreach (var instance in instances)
        {
            using var cnt = new PerformanceCounter("Process", "ID Process", instance, true);
            var val = (int)cnt.RawValue;
            if (val == pid)
            {
                return instance;
            }
        }

        throw new Exception("Could not find performance counter instance name for current process.");
    }

    private static System.Diagnostics.Process GetProcessInstanceByName(string name)
    {
        var cat = new PerformanceCounterCategory("Process");
        string[] instances = cat.GetInstanceNames();

        foreach (var instance in instances)
        {
            if (!instance.Equals(name, StringComparison.CurrentCultureIgnoreCase)) continue;
            using var cnt = new PerformanceCounter("Process", "ID Process", instance, true);
            var val = (int)cnt.RawValue;
            return System.Diagnostics.Process.GetProcessById(val);
        }

        throw new Exception("Could not find performance counter instance name for current process.");
    }

    private static ulong GetTotalPhysicalMemory()
    {
        throw new NotImplementedException();
        // ulong totalMemory = 0;
        //
        // var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
        //
        // foreach (var o in searcher.Get())
        // {
        //     var obj = (ManagementObject)o;
        //     totalMemory = (ulong)obj["TotalPhysicalMemory"];
        // }
        //
        // return totalMemory;
    }

    private static ulong GetAvailablePhysicalMemory()
    {
        throw new NotImplementedException();
        // ulong availableMemory = 0;
        //
        // var searcher = new ManagementObjectSearcher("SELECT FreePhysicalMemory FROM Win32_OperatingSystem");
        //
        // foreach (var o in searcher.Get())
        // {
        //     var obj = (ManagementObject)o;
        //     availableMemory = (ulong)obj["FreePhysicalMemory"] * 1024; // Convert from KB to bytes
        // }
        //
        // return availableMemory;
    }
}