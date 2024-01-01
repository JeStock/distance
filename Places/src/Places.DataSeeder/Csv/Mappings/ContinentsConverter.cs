using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using static Places.DataSeeder.Models.Continent;

namespace Places.DataSeeder.Csv.Mappings;

public class ContinentsConverter<T>() : EnumConverter(typeof(T))
    where T : struct, Enum
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        => text switch
        {
            "AF" => Africa,
            "AN" => Antarctica,
            "AS" => Asia,
            "EU" => Europe,
            "NA" => NorthAmerica,
            "OC" => Oceania,
            "SA" => SouthAmerica,
            _ => Unknown
        };
}