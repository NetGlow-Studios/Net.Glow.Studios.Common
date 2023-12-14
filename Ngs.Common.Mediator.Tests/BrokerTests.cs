using Ngs.Common.Mediator.Broker;

namespace Ngs.Common.Mediator.Tests;

public class BrokerTests
{
    [Fact]
    public void Broker()
    {
        var entity = new PersonEntity("Dawid", "Mika", 12);
        var br = new Broker<PersonEntity, Person>();
        var rs = br.Convert(entity);
        // br.Set(x,y => x.Name == "");
    }
}