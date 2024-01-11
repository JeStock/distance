using CSharpFunctionalExtensions;
using Places.Core.Contracts.Models;
using static Places.Core.ErrorHandling;

namespace Places.Core.Domain;

public readonly record struct Location
{
    public double Longitude { get; }
    public double Latitude { get; }

    private Location(double longitude, double latitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    private static Result<double> ParseLongitude(double? value) =>
        !value.HasValue || Math.Abs(value.Value) > 180
            ? FailWith<double>("Provided Longitude is invalid")
            : value.Value;

    private static Result<double> ParseLatitude(double? value) =>
        !value.HasValue || Math.Abs(value.Value) > 90
            ? FailWith<double>("Provided Latitude is invalid")
            : value.Value;

    public static Result<Location> Parse(LocationDto dto)
    {
        var longitude = ParseLongitude(dto.Longitude);
        var latitude = ParseLatitude(dto.Latitude);

        var result = Combine(longitude, latitude);
        return result.IsFailure
            ? FailWith<Location>(result.Error)
            : new Location(longitude.Value, latitude.Value);
    }

    public LocationDto ToDto() =>
        new() { Latitude = Latitude, Longitude = Longitude };
}