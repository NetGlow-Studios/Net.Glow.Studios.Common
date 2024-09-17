using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Ngs.Common.AspNetCore.Performance.Counters;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public sealed class WindowsDeviceCounter : IDeviceCounter
{
    private readonly System.Diagnostics.PerformanceCounter[] _cpus;
    private readonly System.Diagnostics.PerformanceCounter[] _networks;
    private readonly System.Diagnostics.PerformanceCounter[] _networkUploads;
    private readonly System.Diagnostics.PerformanceCounter[] _networkDownloads;
    private readonly System.Diagnostics.PerformanceCounter[] _disks;
        
    public ulong MemoryUsed { get; }
    public ulong TotalMemory { get; }

    public WindowsDeviceCounter()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            throw new PlatformNotSupportedException("This class is only supported on Windows.");
        }

        try
        {
            var processorCount = Environment.ProcessorCount;
            _cpus = new System.Diagnostics.PerformanceCounter[processorCount];
            for (var i = 0; i < processorCount; i++)
            {
                _cpus[i] = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", i.ToString());
            }

            MemoryUsed = GetAvailablePhysicalMemory();
            TotalMemory = GetTotalPhysicalMemory();

            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni => ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                             ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                             ni.NetworkInterfaceType != NetworkInterfaceType.Unknown &&
                             !ni.Description.Contains("Virtual", StringComparison.OrdinalIgnoreCase) &&
                             !ni.Description.Contains("Hyper-V", StringComparison.OrdinalIgnoreCase) &&
                             !ni.Description.Contains("VMware", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            _networks = new System.Diagnostics.PerformanceCounter[networkInterfaces.Length];
            _networkDownloads = new System.Diagnostics.PerformanceCounter[networkInterfaces.Length];
            _networkUploads = new System.Diagnostics.PerformanceCounter[networkInterfaces.Length];

            for (var i = 0; i < networkInterfaces.Length; i++)
            {
                var networkName = networkInterfaces[i].Description;
                try
                {
                    _networks[i] = new System.Diagnostics.PerformanceCounter("Network Interface", "Bytes Total/sec", networkName);
                    _networkDownloads[i] = new System.Diagnostics.PerformanceCounter("Network Interface", "Bytes Received/sec", networkName);
                    _networkUploads[i] = new System.Diagnostics.PerformanceCounter("Network Interface", "Bytes Sent/sec", networkName);
                }
                catch (Exception ex)
                {
                    // Handle exceptions for specific interfaces
                    Console.WriteLine($"Failed to create PerformanceCounter for network interface {networkName}: {ex.Message}");
                }
            }

            var drives = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Fixed).ToArray();
            _disks = new System.Diagnostics.PerformanceCounter[drives.Length];

            for (var i = 0; i < drives.Length; i++)
            {
                var driveName = FormatDriveName(drives[i].Name);
                _disks[i] = new System.Diagnostics.PerformanceCounter("LogicalDisk", "% Free Space", driveName);
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to initialize performance counters.", ex);
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
        
    private static string FormatDriveName(string driveName)
    {
        return driveName.TrimEnd('\\');
    }

    private void Dispose(bool disposing)
    {
        if (!disposing) return;
            
        foreach (var counter in _cpus) counter.Dispose();
        foreach (var counter in _networks) counter.Dispose();
        foreach (var counter in _networkDownloads) counter.Dispose();
        foreach (var counter in _networkUploads) counter.Dispose();
        foreach (var counter in _disks) counter.Dispose();
    }

    public float GetCpuUsage()
    {
        return _cpus.Average(cpu => cpu.NextValue());
    }

    public float GetCpuUsage(int index)
    {
        return _cpus[index].NextValue();
    }

    public float GetMemoryUsage()
    {
        return MemoryUsed;
    }

    public float GetMemoryPercentageUsage()
    {
        var totalMemory = GetTotalPhysicalMemory();
        var availableMemory = GetAvailablePhysicalMemory();

        if (totalMemory == 0)
        {
            return 0;
        }

        var usedMemory = totalMemory - availableMemory;
        var memoryUsagePercentage = (float)usedMemory / totalMemory * 100;

        return memoryUsagePercentage;
    }

    public float GetNetworkUsage()
    {
        return _networks.Sum(network => network.NextValue());
    }

    public float GetNetworkUsage(string network)
    {
        return _networks.FirstOrDefault(x => x.InstanceName == network)?.NextValue() ?? 0;
    }

    public float GetNetworkUsage(int index)
    {
        return _networks[index].NextValue();
    }

    public float GetNetworkDownload()
    {
        return _networkDownloads.Sum(download => download.NextValue());
    }

    public float GetNetworkDownload(string network)
    {
        return _networkDownloads.FirstOrDefault(x => x.InstanceName == network)?.NextValue() ?? 0;
    }

    public float GetNetworkDownload(int index)
    {
        return _networkDownloads[index].NextValue();
    }

    public float GetNetworkUpload()
    {
        return _networkUploads.Sum(upload => upload.NextValue());
    }

    public float GetNetworkUpload(string network)
    {
        return _networkUploads.FirstOrDefault(x => x.InstanceName == network)?.NextValue() ?? 0;
    }

    public float GetNetworkUpload(int index)
    {
        return _networkUploads[index].NextValue();
    }

    public float GetDiskUsage()
    {
        return _disks.Average(disk => 100 - disk.NextValue());
    }

    public float GetDiskUsage(string drive)
    {
        var formattedDrive = FormatDriveName(drive);
        var disk = _disks.FirstOrDefault(d => d.InstanceName.Equals(formattedDrive, StringComparison.OrdinalIgnoreCase));
        return disk != null ? 100 - disk.NextValue() : 0f;
    }

    public float GetDiskUsage(int index)
    {
        return index >= 0 && index < _disks.Length ? 100 - _disks[index].NextValue() : 0f;
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