namespace Ngs.Common.AspNetCore.Tools.Extensions;

public static class TypeExtensions
{
    public static IEnumerable<Type> GetDerivedTypes(this Type type)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract);
    }
}