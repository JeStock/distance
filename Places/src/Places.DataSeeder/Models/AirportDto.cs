namespace Places.DataSeeder.Models;

public class AirportDto
{
    public int Id { get; init; }
    public string? IcaoCode { get; init; }
    public AirportType Type { get; init; }
    public string? Name { get; init; }
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
    public int? Elevation { get; init; }
    public Continent Continent { get; init; }
    public string? IsoCountry { get; init; }
    public string? IsoRegion { get; init; }
    public string? Municipality { get; init; }
    public ScheduledService ScheduledService { get; init; }
    public string? GpsCode { get; init; }
    public string? IataCode { get; init; }
    public string? LocalCode { get; init; }
    public string? HomeLink { get; init; }
    public string? WikipediaLink { get; init; }
    public string? Keywords { get; init; }
}