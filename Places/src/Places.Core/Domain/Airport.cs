using CSharpFunctionalExtensions;
using Places.Core.Contracts.Models;
using Places.Core.Domain.Enums;
using static Places.Shared.ErrorHandling;

namespace Places.Core.Domain;

public class Airport
{
    public int Id { get; }
    public string Name { get; }
    public Icao Icao { get; }
    public Iata Iata { get; }
    public AirportType Type { get; }
    public Continent Continent { get; }
    public ScheduledService ScheduledService { get; }
    public Location Location { get; }

    private Airport(int id, string name, Icao icao, Iata iata, AirportType type,
        Continent continent, ScheduledService scheduledService, Location location)
    {
        Id = id;
        Name = name;
        Icao = icao;
        Iata = iata;
        Type = type;
        Continent = continent;
        ScheduledService = scheduledService;
        Location = location;
    }

    private static Result<string> ParseName(string? name) =>
        name == null || string.IsNullOrWhiteSpace(name)
            ? FailWith<string>("Provided Name is invalid")
            : name;

    public static Result<Airport> Parse(AirportDto dto)
    {
        var name = ParseName(dto.Name);
        var icao = Icao.Parse(dto.IcaoCode);
        var iata = Iata.Parse(dto.IataCode);
        var type = AirportTypeParser.Parse(dto.Type);
        var continent = ContinentParser.Parse(dto.Continent);
        var service = ScheduledServiceParser.Parse(dto.ScheduledService);
        var location = Location.Parse(dto.Location);

        return Combine(name, icao, iata, type, continent, service, location)
            .Map(() =>
                new Airport(dto.Id, name.Value, icao.Value, iata.Value,
                    type.Value, continent.Value, service.Value, location.Value)
            );
    }

    public AirportDto ToDto() =>
        new()
        {
            Id = Id,
            Name = Name,
            IcaoCode = Icao.Code,
            IataCode = Iata.Code,
            Type = AirportTypeParser.ToString(Type),
            Continent = ContinentParser.ToString(Continent),
            ScheduledService = ScheduledServiceParser.ToString(ScheduledService),
            Location = Location.ToDto(),
        };
}