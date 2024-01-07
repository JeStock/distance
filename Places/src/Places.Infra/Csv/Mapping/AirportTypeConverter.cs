using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Places.Infra.Csv.Models;
using static Places.Infra.Csv.Models.AirportType;

namespace Places.Infra.Csv.Mapping;

public class AirportTypeConverter<T>() : EnumConverter(typeof(T))
    where T : struct, Enum
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) =>
        text switch
        {
            "closed" => Closed,
            "heliport" => Heliport,
            "seaplane_base" => SeaplaneBase,
            "balloonport" => Balloonport,
            "small_airport" => SmallAirport,
            "medium_airport" => MediumAirport,
            "large_airport" => LargeAirport,
            _ => (AirportType?)default
        };
}