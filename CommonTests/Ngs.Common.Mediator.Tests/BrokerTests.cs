using Ngs.Common.Mediator.Broker;

namespace Ngs.Common.Mediator.Tests;

public class BrokerTests
{
    [Fact]
    public void Broker()
    {
        var entity = new PersonEntity("", "", 8);
        var br = new Broker<PersonEntity, Person>();
        var rs = br.Convert(entity);
        // br.Set(x,y => x.Name == "");
    }
}