using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Places.Infra.Csv.Models;
using static Places.Infra.Csv.Models.ScheduledService;

namespace Places.Infra.Csv.Mapping;

public class ScheduledServiceConverter<T>() : EnumConverter(typeof(T))
    where T : struct, Enum
{
    public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) =>
        text switch
        {
            "yes" => Yes,
            "no" => No,
            _ => (ScheduledService?)default
        };
}