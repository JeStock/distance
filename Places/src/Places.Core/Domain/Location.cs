using Places.Core.Contracts.Models;

namespace Places.Core.Domain;

public record struct Location
{
    public double Longitude { get; }
    public double Latitude { get; }

    private Location(double longitude, double latitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Location? Create(LocationDto dto)
    {
        if (!dto.Longitude.HasValue || Math.Abs(dto.Longitude.Value) > 180)
            return default;

        if (!dto.Latitude.HasValue || Math.Abs(dto.Latitude.Value) > 90)
            return default;

        return new Location(dto.Longitude.Value, dto.Latitude.Value);
    }

    public LocationDto ToDto()
    {
        return new LocationDto
        {
            Latitude = Latitude,
            Longitude = Longitude
        };
    }
}