using Places.Core.Contracts.Models;
using Places.Core.Domain.Enums;
using AirportType = Places.Core.Domain.Enums.AirportType;
using Continent = Places.Core.Domain.Enums.Continent;
using ScheduledService = Places.Core.Domain.Enums.ScheduledService;

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

    public static Airport? Create(AirportDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            return default;

        var icao = Icao.Create(dto.IcaoCode);
        if (icao == null)
            return default;

        var iata = Iata.Create(dto.IataCode);
        if (iata == null)
            return default;

        var type = dto.Type.ToDomain();
        if (!type.HasValue)
            return default;

        var continent = dto.Continent.MapToDomain();
        if (continent == null)
            return default;

        var service = dto.ScheduledService.MapToDomain();
        if (service == null)
            return default;

        var location = Location.Create(dto.Location);
        if (!location.HasValue)
            return default;

        return new Airport(dto.Id, dto.Name, icao, iata,
            type.Value, continent.Value, service.Value, location.Value);
    }

    public AirportDto ToDto() =>
        new()
        {
            Id = Id,
            Name = Name,
            IcaoCode = Icao.Code,
            IataCode = Iata.Code,
            Type = Type.ToDto(),
            Continent = Continent.ToDto(),
            ScheduledService = ScheduledService.ToDto(),
            Location = Location.ToDto(),
        };
}