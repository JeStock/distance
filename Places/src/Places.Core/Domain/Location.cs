﻿using CSharpFunctionalExtensions;
using Places.Core.Contracts.Models;
using static Places.Shared.ErrorHandling;

namespace Places.Core.Domain;

public readonly record struct Location
{
    public double Latitude { get; }
    public double Longitude { get; }

    private Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    private static Result<double> ParseLatitude(double? value) =>
        !value.HasValue || Math.Abs(value.Value) > 90
            ? FailWith<double>("Provided Latitude is invalid")
            : value.Value;

    private static Result<double> ParseLongitude(double? value) =>
        !value.HasValue || Math.Abs(value.Value) > 180
            ? FailWith<double>("Provided Longitude is invalid")
            : value.Value;

    public static Result<Location> Parse(LocationDto dto)
    {
        var latitude = ParseLatitude(dto.Latitude);
        var longitude = ParseLongitude(dto.Longitude);

        var result = Combine(latitude, longitude);
        return result.IsFailure
            ? FailWith<Location>(result.Error)
            : new Location(longitude.Value, latitude.Value);
    }

    public LocationDto ToDto() =>
        new() { Latitude = Latitude, Longitude = Longitude };
}