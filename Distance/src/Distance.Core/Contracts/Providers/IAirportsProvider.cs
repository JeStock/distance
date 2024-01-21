using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Domain;

namespace Distance.Core.Contracts.Providers;

public interface IAirportsProvider
{
    Task<Maybe<AirportDto>> GetAirportAsync(string iata, CancellationToken token = default);
    Task<Result<(AirportDto Origin, AirportDto Destination)>> GetItineraryAirportsAsync(
        Itinerary itinerary, CancellationToken token = default);
}