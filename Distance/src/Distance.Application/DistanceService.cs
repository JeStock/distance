using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Providers;
using Distance.Core.Contracts.Services;
using Distance.Core.Domain;
using static Distance.Shared.ErrorHandling;

namespace Distance.Application;

public class DistanceService : IDistanceService
{
    private readonly IAirportsProvider airportsProvider;

    public DistanceService(IAirportsProvider airportsProvider)
    {
        this.airportsProvider = airportsProvider;
    }

    public async Task<Result<ItineraryDistance>> GetDistanceAsync(Itinerary itinerary, CancellationToken token = default)
    {
        var result = await airportsProvider.GetItineraryAirportsAsync(itinerary, token);

        return result.IsFailure
            ? FailWith<ItineraryDistance>(result.Error)
            : ItineraryDistance.Parse(result.Value.Origin, result.Value.Destination);
    }
}