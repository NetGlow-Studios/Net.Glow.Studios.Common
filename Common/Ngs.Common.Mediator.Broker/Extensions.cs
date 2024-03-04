namespace Ngs.Common.Mediator.Broker;

public static class Extensions
{
    public static Broker<TSource, TOutput> Set<TSource, TOutput>(this Broker<TSource, TOutput> broker, Func<TSource, TOutput, bool> predict)
    {
        
        
        return broker;
    }
}