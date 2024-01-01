using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using static Places.DataSeeder.Models.ScheduledService;

namespace Places.DataSeeder.Csv.Mappings;

public class ScheduledServiceConverter<T>() : EnumConverter(typeof(T))
    where T : struct, Enum
{
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        => text switch
        {
            "yes" => Yes,
            "no" => No,
            _ => Unknown
        };
}