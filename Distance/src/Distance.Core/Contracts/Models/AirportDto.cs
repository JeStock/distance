namespace Distance.Core.Contracts.Models;

public class AirportDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string IcaoCode { get; init; }
    public required string IataCode { get; init; }
    public required string Type { get; init; }
    public required string Continent { get; init; }
    public required string ScheduledService { get; init; }
    public required LocationDto Location { get; init; }
}