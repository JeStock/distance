namespace Places.Core.Contracts.Models;

public class AirportDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? IcaoCode { get; init; }
    public string? IataCode { get; init; }
    public string? Type { get; init; }
    public string? Continent { get; init; }
    public string? ScheduledService { get; init; }
    public LocationDto Location { get; init; } = new();
}