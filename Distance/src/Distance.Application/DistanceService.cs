using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Core.Contracts.Services;
using Distance.Core.Domain;
using static Distance.Shared.ErrorHandling;

namespace Distance.Application;

public class DistanceService(IAirportsProvider airportsProvider) : IDistanceService
{
    public async Task<Result<DistanceDto>> GetDistanceAsync(Itinerary itinerary, CancellationToken token = default)
    {
        if (itinerary.Origin == itinerary.Destination)
            return new DistanceDto(0);

        var (origin, destination) = await QueryAirportsAsync(itinerary, token);

        var airportsCombinedResult = Combine(origin, destination);
        if (airportsCombinedResult.IsFailure)
            return Result.Failure<DistanceDto>(airportsCombinedResult.Error);

        var originLocation = Location.Parse(origin.Value.Location);
        var destinationLocation = Location.Parse(destination.Value.Location);

        var locationsCombinedResult = Combine(originLocation, destinationLocation);
        return locationsCombinedResult.IsFailure
            ? Result.Failure<DistanceDto>(locationsCombinedResult.Error)
            : originLocation.Value.DistanceTo(destinationLocation.Value);
    }

    private async Task<(Result<AirportDto>, Result<AirportDto>)> QueryAirportsAsync(
        Itinerary itinerary, CancellationToken token = default)
    {
        var originTask = airportsProvider.GetAirportByIataAsync(itinerary.Origin.Code, token);
        var destinationTask = airportsProvider.GetAirportByIataAsync(itinerary.Destination.Code, token);

        await Task.WhenAll(originTask, destinationTask);

        var originAirport = await originTask.ToResult($"{itinerary.Origin.Code} airport not found");
        var destinationAirport = await destinationTask.ToResult($"{itinerary.Destination.Code} airport not found");

        return (originAirport, destinationAirport);
    }
}