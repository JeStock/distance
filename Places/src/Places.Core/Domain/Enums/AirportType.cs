namespace Places.Core.Domain.Enums;

public enum AirportType
{
    Heliport = 1,
    SmallAirport = 2,
    MediumAirport = 3,
    LargeAirport = 4
}

public static class AirportTypeExtensions
{
    public static AirportType? ToDomain(this Contracts.Models.AirportType? type) =>
        type switch
        {
            Contracts.Models.AirportType.Heliport => AirportType.Heliport,
            Contracts.Models.AirportType.SmallAirport => AirportType.SmallAirport,
            Contracts.Models.AirportType.MediumAirport => AirportType.MediumAirport,
            Contracts.Models.AirportType.LargeAirport => AirportType.LargeAirport,
            _ => (AirportType?)default
        };

    public static Contracts.Models.AirportType ToDto(this AirportType type) =>
        type switch
        {
            AirportType.Heliport => Contracts.Models.AirportType.Heliport,
            AirportType.SmallAirport => Contracts.Models.AirportType.SmallAirport,
            AirportType.MediumAirport => Contracts.Models.AirportType.MediumAirport,
            AirportType.LargeAirport => Contracts.Models.AirportType.LargeAirport
        };
}