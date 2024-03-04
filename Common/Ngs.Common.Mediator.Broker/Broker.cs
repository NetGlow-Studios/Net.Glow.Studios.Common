namespace Ngs.Common.Mediator.Broker;

public class Broker<TSource, TOutput>
{
    

    public TOutput Convert(TSource source)
    {
        var sourceProps = typeof(TSource).GetProperties();
        var outputProps = typeof(TOutput).GetProperties();
        
        var result = Activator.CreateInstance<TOutput>()!;

        foreach (var outputProp in outputProps)
        {
            var sourceProp = sourceProps.FirstOrDefault(x => x.Name == outputProp.Name);
            
            if(sourceProp == null) break;

            var sourcePropVal = sourceProp.GetValue(source);
            var currentResultProp = result.GetType().GetProperty(sourceProp.Name);
            currentResultProp!.SetValue(result, sourcePropVal);
        }
        
        return result;
    }
}