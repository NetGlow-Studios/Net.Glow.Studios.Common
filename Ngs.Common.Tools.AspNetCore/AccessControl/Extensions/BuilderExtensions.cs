using Microsoft.Extensions.DependencyInjection;
using Ngs.Common.Tools.AspNetCore.AccessControl.Filters;
using Ngs.Common.Tools.AspNetCore.AccessControl.Interfaces;

namespace Ngs.Common.Tools.AspNetCore.AccessControl.Extensions;

/// <summary>
/// Extensions for asp builder.
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Add user privilege auth.
    /// </summary>
    /// <param name="services"> Services to add user privilege auth to. </param>
    /// <typeparam name="TIPrivilege"> Type of the service privilege. </typeparam>
    /// <returns></returns>
    public static IServiceCollection AddUserPrivilegeAuth<TIPrivilege>(this IServiceCollection services) where TIPrivilege : class, IPrivilege
    {
        services.AddScoped<HasPrivilegeFilter<TIPrivilege>>(x =>
            new HasPrivilegeFilter<TIPrivilege>(x.GetRequiredService<TIPrivilege>(), default!, default!, default!, default!));

        return services;
    }
}