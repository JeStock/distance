namespace Places.Core.Contracts.Models;

// TODO [sg]: Csv namespace have their own types. Add explanation why
public class AirportDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? IcaoCode { get; init; }
    public string? IataCode { get; init; }
    public AirportType? Type { get; init; }
    public Continent? Continent { get; init; }
    public ScheduledService? ScheduledService { get; init; }
    public LocationDto Location { get; init; } = new();
}