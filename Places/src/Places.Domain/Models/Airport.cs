using Places.Domain.Enums;

namespace Places.Domain.Models;

public class Airport
{
    public required int Id { get; init; }
    public required string IcaoCode { get; init; }
    public required string IataCode { get; init; }
    public required AirportType Type { get; init; }
    public required Continent Continent { get; init; }
    public required ScheduledService ScheduledService { get; init; }
    public required string Name { get; init; }
    public required Location Location { get; init; }
}