using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.AspNetCore.AccessControl.Filters;
using Ngs.Common.AspNetCore.AccessControl.Interfaces;

namespace Ngs.Common.AspNetCore.AccessControl.Extensions;

/// <summary>
/// Extensions for asp builder.
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Add user privilege authentication for the endpoints.
    /// </summary>
    /// <param name="services"> Services to add user privilege auth to. </param>
    /// <typeparam name="TIPrivilege"> Type of the service privilege. </typeparam>
    /// <returns></returns>
    public static IServiceCollection AddUserPrivilegeAuth<TIPrivilege>(this IServiceCollection services) where TIPrivilege : class, IPrivilege
    {
        services.AddScoped<AccessControlFilter<TIPrivilege>>(x 
            => new AccessControlFilter<TIPrivilege>(x.GetRequiredService<TIPrivilege>(), 
                default!, default!, default!, default!));

        return services;
    }
}