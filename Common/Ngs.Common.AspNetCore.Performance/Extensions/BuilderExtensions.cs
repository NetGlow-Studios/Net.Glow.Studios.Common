using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.Performance.Counters;

namespace Ngs.Common.AspNetCore.Performance.Extensions;

public static class BuilderExtensions
{
    public static IServiceCollection AddPerformanceCounters(this IServiceCollection services)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            services.AddSingleton<IDeviceCounter, WindowsDeviceCounter>();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            services.AddSingleton<IDeviceCounter, LinuxDeviceCounter>();
        }
        else
        {
            throw new PlatformNotSupportedException("This platform is not supported to get performance stats.");
        }
        
        return services;
    }
}