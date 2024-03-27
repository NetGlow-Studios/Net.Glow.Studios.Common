using Microsoft.EntityFrameworkCore;
using Ngs.Common.AspNetCore.Infrastructure.Repositories;

namespace Ngs.Common.AspNetCore.Infrastructure.Example.Repositories;

//Example of repository class for ExampleEntity
public class ExampleRepository(ExampleDbContext applicationDbContext) : BaseRepositoryAsync<ExampleEntity>(applicationDbContext), IExampleRepository
{
    //Example of custom method to get an entity by name.
    public async Task<ExampleEntity> GetByNameAsync(string name)
    {
        return await applicationDbContext.Set<ExampleEntity>().SingleAsync(x => x.Name == name);
    }
}