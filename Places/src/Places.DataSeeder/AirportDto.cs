namespace Places.DataSeeder;

public class AirportDto
{
    public int Id { get; set; }
    public string? Icao { get; set; }
    public string? Type { get; set; } // TODO [sg]: probably enum
    public string? Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public int? Elevation { get; set; }
    public string? Continent { get; set; } // TODO [sg]: probably enum
    public string? IsoCountry { get; set; }
    public string? IsoRegion { get; set; }
    public string? Municipality { get; set; }
    public string? ScheduledService { get; set; } // TODO [sg]: probably just "yes"/"no"
    public string? GpsCode { get; set; }
    public string? IataCode { get; set; }
    public string? LocalCode { get; set; }
    public string? HomeLink { get; set; }
    public string? WikipediaLink { get; set; }
    public string? Keywords { get; set; }
}