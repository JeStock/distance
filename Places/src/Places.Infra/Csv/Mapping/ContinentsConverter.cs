using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Places.Infra.Csv.Models;
using static Places.Infra.Csv.Models.Continent;

namespace Places.Infra.Csv.Mapping;

public class ContinentsConverter<T>() : EnumConverter(typeof(T))
    where T : struct, Enum
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) =>
        text switch
        {
            "AF" => Africa,
            "AN" => Antarctica,
            "AS" => Asia,
            "EU" => Europe,
            "NA" => NorthAmerica,
            "OC" => Oceania,
            "SA" => SouthAmerica,
            _ => (Continent?)default
        };
}