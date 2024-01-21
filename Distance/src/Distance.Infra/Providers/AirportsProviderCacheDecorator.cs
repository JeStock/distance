using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Core.Domain;
using Distance.Infra.Repositories;
using Microsoft.Extensions.Logging;

namespace Distance.Infra.Providers;

public class AirportsProviderCacheDecorator(
    IAirportsProvider airportsProvider,
    IRepository<AirportDto> repository,
    ILogger<AirportsProviderCacheDecorator> logger)
    : IAirportsProvider
{
    public async Task<Maybe<AirportDto>> GetAirportAsync(string iata, CancellationToken token = default)
    {
        var cached = await repository.GetAsync(iata);
        if (cached != null)
        {
            logger.LogInformation("Airport {Iata} found in cache", iata);
            return cached;
        }

        var response = await airportsProvider.GetAirportAsync(iata, token);
        if (response.HasValue)
        {
            logger.LogInformation("Caching airport {Iata}", iata);
            await repository.SetAsync(iata, response.Value);
        }

        return response;
    }

    public Task<Result<(AirportDto Origin, AirportDto Destination)>> GetItineraryAirportsAsync(
        Itinerary itinerary, CancellationToken token = default) =>
        airportsProvider.GetItineraryAirportsAsync(itinerary, token);
}