using CsvHelper.Configuration;
using Places.Core.Contracts.Models;

namespace Places.Infra.Csv.Mapping;

public sealed class AirportDtoMap : ClassMap<AirportDto>
{
    public AirportDtoMap()
    {
        Map(m => m.Id).Name("id");
        Map(m => m.IcaoCode).Name("ident");
        Map(m => m.Type).Name("type");
        Map(m => m.Name).Name("name");
        Map(m => m.Location.Longitude).Name("longitude_deg");
        Map(m => m.Location.Latitude).Name("latitude_deg");
        Map(m => m.Continent).Name("continent");
        Map(m => m.ScheduledService).Name("scheduled_service");
        Map(m => m.IataCode).Name("iata_code");
    }
}