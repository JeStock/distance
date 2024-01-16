using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Core.Contracts.Services;
using Distance.Core.Domain;

namespace Distance.Infra.Services;

public class DistanceService(IAirportsProvider airportsProvider) : IDistanceService
{
    public async Task<Result<DistanceDto>> GetDistanceAsync(ItineraryDto dto, CancellationToken token)
    {
        if (dto.Origin == dto.Destination)
            return new DistanceDto(0);

        var origin = airportsProvider.GetAsync(dto.Origin, token);
        var destination = airportsProvider.GetAsync(dto.Destination, token);

        var results = await Task.WhenAll(origin, destination);

        /*var originCoordinate = origin.Result.

        return GeoCalculator.GetDistance(origin.Result, destination.Result)*/
        throw new NotImplementedException();
    }

    // TODO [sg]: to automapper or smth
    /*private Coordinate BuildCoordinate(PlaceDto place)
    {
        return new Coordinate(place.Location.Lat, place.Location.Lon);
    }*/
}