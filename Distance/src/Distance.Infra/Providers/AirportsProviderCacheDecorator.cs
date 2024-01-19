using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Infra.Repositories;

namespace Distance.Infra.Providers;

public class AirportsProviderCacheDecorator(
    IAirportsProvider airportsProvider,
    IRepository<AirportDto> repository)
    : IAirportsProvider
{
    public async Task<Maybe<AirportDto>> GetAirportByIataAsync(string iata, CancellationToken token)
    {
        var cached = await repository.GetAsync(iata, token);
        if (cached != null)
            return cached;

        var response = await airportsProvider.GetAirportByIataAsync(iata, token);
        if (response.HasValue)
            await repository.SetAsync(iata, response.Value, token);

        return response;
    }
}