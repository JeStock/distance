using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Geolocation;
using static Distance.Shared.ErrorHandling;

namespace Distance.Core.Domain;

public readonly record struct Location
{
    public double Latitude { get; }
    public double Longitude { get; }

    private Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public DistanceDto DistanceTo(Location other) =>
        new()
        {
            Units = DistanceUnit.Kilometers.ToString(),
            Distance = GeoCalculator.GetDistance(
                new Coordinate(Latitude, Longitude),
                new Coordinate(other.Latitude, other.Longitude),
                distanceUnit: DistanceUnit.Kilometers)
        };

    private static Result<double> ParseLatitude(double? value) =>
        !value.HasValue || Math.Abs(value.Value) > 90
            ? FailWith<double>("Provided Latitude is invalid")
            : value.Value;

    private static Result<double> ParseLongitude(double? value) =>
        !value.HasValue || Math.Abs(value.Value) > 180
            ? FailWith<double>("Provided Longitude is invalid")
            : value.Value;

    public static Result<Location> Parse(LocationDto dto) =>
        Parse(dto.Latitude, dto.Longitude);

    public static Result<Location> Parse(double latitude, double longitude)
    {
        var latitudeParseResult = ParseLatitude(latitude);
        var longitudeParseResult = ParseLongitude(longitude);

        var result = Combine(latitudeParseResult, longitudeParseResult);
        return result.IsFailure
            ? FailWith<Location>(result.Error)
            : new Location(latitudeParseResult.Value, longitudeParseResult.Value);
    }
}