using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Places.Core.Contracts.Models;
using static Places.Core.Contracts.Models.ScheduledService;

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