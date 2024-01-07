using Places.Infra.Csv.Models;

namespace Places.Infra.Csv.Mapping;

public static class MappingExtensions
{
    public static Domain.Enums.AirportType? MapToDomain(this AirportType? type) =>
        type switch
        {
            AirportType.Heliport => Domain.Enums.AirportType.Heliport,
            AirportType.SmallAirport => Domain.Enums.AirportType.SmallAirport,
            AirportType.MediumAirport => Domain.Enums.AirportType.MediumAirport,
            AirportType.LargeAirport => Domain.Enums.AirportType.LargeAirport,
            _ => (Domain.Enums.AirportType?)default
        };

    public static Domain.Enums.Continent? MapToDomain(this Continent? continent) =>
        continent switch
        {
            Continent.Africa => Domain.Enums.Continent.Africa,
            Continent.Antarctica => Domain.Enums.Continent.Antarctica,
            Continent.Asia => Domain.Enums.Continent.Asia,
            Continent.Europe => Domain.Enums.Continent.Europe,
            Continent.NorthAmerica => Domain.Enums.Continent.NorthAmerica,
            Continent.Oceania => Domain.Enums.Continent.Oceania,
            Continent.SouthAmerica => Domain.Enums.Continent.SouthAmerica,
            _ => (Domain.Enums.Continent?)default
        };

    public static Domain.Enums.ScheduledService? MapToDomain(this ScheduledService? service) =>
        service switch
        {
            ScheduledService.Yes => Domain.Enums.ScheduledService.Yes,
            ScheduledService.No => Domain.Enums.ScheduledService.No,
            _ => (Domain.Enums.ScheduledService?)default
        };
}