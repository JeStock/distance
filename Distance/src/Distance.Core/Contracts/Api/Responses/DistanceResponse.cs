namespace Distance.Core.Contracts.Api.Responses;

public class DistanceResponse
{
    public required string OriginAirportName { get; init; }
    public required string DestinationAirportName { get; init; }
    public required double Distance { get; init; }
    public required string UnitsOfMeasure { get; init; }

    public static DistanceResponse FromDomain(Domain.Distance domain) =>
        new()
        {
            OriginAirportName = domain.OriginAirportName,
            DestinationAirportName = domain.DestinationAirportName,
            Distance = domain.DistanceValue,
            UnitsOfMeasure = domain.Units
        };
}