using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using static Places.DataSeeder.Models.AirportType;

namespace Places.DataSeeder.Csv.Mappings;

public class AirportTypeConverter<T>() : EnumConverter(typeof(T))
    where T : struct, Enum
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        => text switch
        {
            "closed" => Closed,
            "heliport" => Heliport,
            "seaplane_base" => SeaplaneBase,
            "balloonport" => Balloonport,
            "small_airport" => SmallAirport,
            "medium_airport" => MediumAirport,
            "large_airport" => LargeAirport,
            _ => Unknown
        };
}