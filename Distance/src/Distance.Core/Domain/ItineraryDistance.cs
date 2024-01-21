using CSharpFunctionalExtensions;
using Distance.Core.Contracts;
using Distance.Core.Contracts.Models;
using Geolocation;
using static Distance.Shared.ErrorHandling;

namespace Distance.Core.Domain;

public class ItineraryDistance
{
    public string OriginAirportName { get; }
    public string DestinationAirportName { get; }
    public double DistanceValue { get; }
    public string Units { get; }

    private ItineraryDistance(double distance, string originAirportName, string destinationAirportName)
    {
        DistanceValue = distance;
        OriginAirportName = originAirportName;
        DestinationAirportName = destinationAirportName;
        Units = DistanceUnitHelper.ToString(DistanceUnit.Kilometers);
    }

    public static Result<ItineraryDistance> Parse(AirportDto origin, AirportDto destination)
    {
        var originLocation = Location.Parse(origin.Location);
        var destinationLocation = Location.Parse(destination.Location);

        return Combine(originLocation, destinationLocation)
            .Map(() => new ItineraryDistance(
                originLocation.Value.DistanceTo(destinationLocation.Value),
                origin.Name, destination.Name)
            );
    }
}