using Distance.Core.Contracts.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Distance.Infra.Repositories;

public class AirportsRepository(IRedisDatabase cache) : IRepository<AirportDto>
{
    public Task<AirportDto?> GetAsync(string key) =>
        cache.GetAsync<AirportDto?>(key);

    public Task SetAsync(string key, AirportDto value) =>
        cache.AddAsync(key, value);
}