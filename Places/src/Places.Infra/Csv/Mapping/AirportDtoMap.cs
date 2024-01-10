using CsvHelper.Configuration;
using Places.Core.Contracts.Models;

namespace Places.Infra.Csv.Mapping;

public sealed class AirportDtoMap : ClassMap<AirportDto>
{
    public AirportDtoMap()
    {
        Map(m => m.Id).Name("id");
        Map(m => m.IcaoCode).Name("ident");
        Map(m => m.Type).Name("type").TypeConverter<AirportTypeConverter<AirportType>>();
        Map(m => m.Name).Name("name");
        Map(m => m.Location.Longitude).Name("longitude_deg");
        Map(m => m.Location.Latitude).Name("latitude_deg");
        Map(m => m.Continent).Name("continent").TypeConverter<ContinentsConverter<Continent>>();
        Map(m => m.ScheduledService).Name("scheduled_service")
            .TypeConverter<ScheduledServiceConverter<ScheduledService>>();
        Map(m => m.IataCode).Name("iata_code");
    }
}