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
    public async Task<Maybe<AirportDto>> GetAsync(string iata, CancellationToken token)
    {
        var cached = await repository.GetAsync(iata, token);
        if (cached != null)
            return cached;

        var placeDto = await airportsProvider.GetAsync(iata, token);
        await repository.SetAsync(iata, null, token);

        return placeDto;
    }
}