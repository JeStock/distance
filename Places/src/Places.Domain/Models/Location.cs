using System.Diagnostics.CodeAnalysis;

namespace Places.Domain.Models;

public class Location
{
    public required double Longitude { get; init; }
    public required double Latitude { get; init; }

    [SetsRequiredMembers]
    public Location(double longitude, double latitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}