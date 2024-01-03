using CsvHelper.Configuration;
using Places.DataSeeder.Models;

namespace Places.DataSeeder.Csv.Mappings;

public sealed class AirportDtoMap : ClassMap<AirportDto>
{
    public AirportDtoMap()
    {
        Map(m => m.Id).Name("id");
        Map(m => m.IcaoCode).Name("ident");
        Map(m => m.Type).Name("type")
            .TypeConverter<AirportTypeConverter<AirportType>>();
        Map(m => m.Name).Name("name");
        Map(m => m.Latitude).Name("latitude_deg");
        Map(m => m.Longitude).Name("longitude_deg");
        Map(m => m.Elevation).Name("elevation_ft");
        Map(m => m.Continent).Name("continent")
            .TypeConverter<ContinentsConverter<Continent>>();
        Map(m => m.IsoCountry).Name("iso_country");
        Map(m => m.IsoRegion).Name("iso_region");
        Map(m => m.Municipality).Name("municipality");
        Map(m => m.ScheduledService).Name("scheduled_service")
            .TypeConverter<ScheduledServiceConverter<ScheduledService>>();
        Map(m => m.GpsCode).Name("gps_code");
        Map(m => m.IataCode).Name("iata_code");
        Map(m => m.LocalCode).Name("local_code");
        Map(m => m.HomeLink).Name("home_link");
        Map(m => m.WikipediaLink).Name("wikipedia_link");
        Map(m => m.Keywords).Name("keywords");
    }
}