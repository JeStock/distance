using Swashbuckle.AspNetCore.Annotations;

namespace Distance.Api.Models.Responses;

public class DistanceResponse
{
    [SwaggerSchema("Departure Airport's name")]
    public required string OriginAirportName { get; init; }

    [SwaggerSchema("Arrival Airport's name")]
    public required string DestinationAirportName { get; init; }

    [SwaggerSchema("Geodesic distance")]
    public required double Distance { get; init; }

    [SwaggerSchema("Units of measure (km)")]
    public required string UnitsOfMeasure { get; init; }

    public static DistanceResponse FromDomain(Core.Domain.ItineraryDistance domain) =>
        new()
        {
            OriginAirportName = domain.OriginAirportName,
            DestinationAirportName = domain.DestinationAirportName,
            Distance = domain.DistanceValue,
            UnitsOfMeasure = domain.Units
        };
}