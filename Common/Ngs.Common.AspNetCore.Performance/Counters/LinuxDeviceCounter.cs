using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace Ngs.Common.AspNetCore.Performance.Counters
{
    public sealed class LinuxDeviceCounter : IDeviceCounter
    {
        public ulong TotalMemory { get; private set; }
        public ulong MemoryUsed => TotalMemory - GetAvailableMemory();

        public LinuxDeviceCounter()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                throw new PlatformNotSupportedException("This class is only supported on Linux.");
            }

            TotalMemory = GetTotalMemory();
        }

        public float GetCpuUsage()
        {
            return GetCpuUsage(0);
        }

        public float GetCpuUsage(int index)
        {
            var cpuUsage = File.ReadAllLines("/proc/stat")
                               .FirstOrDefault(line => line.StartsWith(index == 0 ? "cpu " : $"cpu{index}"));

            if (cpuUsage == null) return 0;

            var parts = cpuUsage.Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(float.Parse).ToArray();
            var idleTime = parts[3] + parts[4];
            var totalTime = parts.Sum();

            return 100 * (1 - idleTime / totalTime);
        }

        public float GetMemoryUsage()
        {
            return (float)MemoryUsed / 1024 / 1024; // Convert to MB
        }

        public float GetMemoryPercentageUsage()
        {
            return 100 * (float)MemoryUsed / TotalMemory;
        }

        public float GetNetworkUsage()
        {
            return GetNetworkDownload() + GetNetworkUpload();
        }

        public float GetNetworkUsage(string network)
        {
            return GetNetworkDownload(network) + GetNetworkUpload(network);
        }

        public float GetNetworkUsage(int index)
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            if (index < 0 || index >= interfaces.Length) return 0;

            var ni = interfaces[index];
            return GetNetworkDownload(ni.Name) + GetNetworkUpload(ni.Name);
        }

        public float GetNetworkDownload()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                                   .Sum(ni => GetNetworkDownload(ni.Name));
        }

        public float GetNetworkDownload(string network)
        {
            var rxPath = $"/sys/class/net/{network}/statistics/rx_bytes";
            return File.Exists(rxPath) ? float.Parse(File.ReadAllText(rxPath)) : 0;
        }

        public float GetNetworkDownload(int index)
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            return index < 0 || index >= interfaces.Length ? 0 : GetNetworkDownload(interfaces[index].Name);
        }

        public float GetNetworkUpload()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                                   .Sum(ni => GetNetworkUpload(ni.Name));
        }

        public float GetNetworkUpload(string network)
        {
            var txPath = $"/sys/class/net/{network}/statistics/tx_bytes";
            return File.Exists(txPath) ? float.Parse(File.ReadAllText(txPath)) : 0;
        }

        public float GetNetworkUpload(int index)
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            return index < 0 || index >= interfaces.Length ? 0 : GetNetworkUpload(interfaces[index].Name);
        }

        public float GetDiskUsage()
        {
            return DriveInfo.GetDrives()
                            .Where(d => d.IsReady && d.DriveType == DriveType.Fixed)
                            .Average(d => 100 - ((float)d.AvailableFreeSpace / d.TotalSize * 100));
        }

        public float GetDiskUsage(string drive)
        {
            var di = DriveInfo.GetDrives().FirstOrDefault(d => d.IsReady && d.Name.Equals(drive, StringComparison.OrdinalIgnoreCase));
            return di == null ? 0 : 100 - ((float)di.AvailableFreeSpace / di.TotalSize * 100);
        }

        public float GetDiskUsage(int index)
        {
            var drives = DriveInfo.GetDrives().Where(d => d.IsReady && d.DriveType == DriveType.Fixed).ToArray();
            return index < 0 || index >= drives.Length ? 0 : 100 - ((float)drives[index].AvailableFreeSpace / drives[index].TotalSize * 100);
        }

        private static ulong GetTotalMemory()
        {
            var memInfo = File.ReadAllLines("/proc/meminfo");
            var totalMemoryLine = memInfo.FirstOrDefault(line => line.StartsWith("MemTotal:"));

            return totalMemoryLine != null ? ulong.Parse(totalMemoryLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]) * 1024 : 0;
        }

        private static ulong GetAvailableMemory()
        {
            var memInfo = File.ReadAllLines("/proc/meminfo");
            var availableMemoryLine = memInfo.FirstOrDefault(line => line.StartsWith("MemAvailable:"));

            return availableMemoryLine != null ? ulong.Parse(availableMemoryLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]) * 1024 : 0;
        }

        public void Dispose()
        {
            // Dispose resources if needed
        }
    }
}
