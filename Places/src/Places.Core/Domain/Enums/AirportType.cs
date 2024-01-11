using CSharpFunctionalExtensions;
using static Places.Core.Domain.Enums.AirportType;
using static Places.Core.ErrorHandling;

namespace Places.Core.Domain.Enums;

public enum AirportType
{
    Heliport = 1,
    SmallAirport = 2,
    MediumAirport = 3,
    LargeAirport = 4
}

public static class AirportTypeParser
{
    public static Result<AirportType> Parse(string? typeStr) =>
        typeStr switch
        {
            "heliport" => Heliport,
            "small_airport" => SmallAirport,
            "medium_airport" => MediumAirport,
            "large_airport" => LargeAirport,
            _ => FailWith<AirportType>("Airport type is invalid")
        };

    public static string ToString(this AirportType type) =>
        type switch
        {
            Heliport => "heliport",
            SmallAirport => "small_airport",
            MediumAirport => "medium_airport",
            LargeAirport => "large_airport",
        };
}