using Distance.Core.Contracts.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Distance.Infra.Repositories;

public class AirportsRepository(IRedisDatabase cache) : IRepository<AirportDto>
{
    public async Task<AirportDto?> GetAsync(string key, CancellationToken token)
    {
        var res = await cache.AddAsync("my cache key", 1);
        var test = await cache.GetAsync<string>("my cache key");

        throw new NotImplementedException();
    }

    public Task SetAsync(string key, AirportDto value, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}