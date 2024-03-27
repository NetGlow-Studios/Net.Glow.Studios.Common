namespace Ngs.Common.AspNetCore.Infrastructure.Example.Repositories;

public interface IExampleRepository
{
    public Task<ExampleEntity> GetByNameAsync(string name);
}