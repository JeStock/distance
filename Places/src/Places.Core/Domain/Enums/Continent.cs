using CSharpFunctionalExtensions;
using static Places.Core.Domain.Enums.Continent;
using static Places.Core.ErrorHandling;

namespace Places.Core.Domain.Enums;

public enum Continent
{
    Africa = 1,
    Antarctica = 2,
    Asia = 3,
    Europe = 4,
    NorthAmerica = 5,
    Oceania = 6,
    SouthAmerica = 7
}

public static class ContinentParser
{
    public static Result<Continent> Parse(string? continentStr) =>
        continentStr switch
        {
            "AF" => Africa,
            "AN" => Antarctica,
            "AS" => Asia,
            "EU" => Europe,
            "NA" => NorthAmerica,
            "OC" => Oceania,
            "SA" => SouthAmerica,
            _ => FailWith<Continent>("Continent code is invalid")
        };

    public static string ToString(this Continent continent) =>
        continent switch
        {
            Africa => "AF",
            Antarctica => "AN",
            Asia => "AS",
            Europe => "EU",
            NorthAmerica => "NA",
            Oceania => "OC",
            SouthAmerica => "SA"
        };
}