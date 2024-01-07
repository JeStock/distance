using Places.Domain.Models;
using Places.Infra.Csv.Mapping;
using Places.Infra.Csv.Models;
using Places.Shared;
using static Places.Domain.DomainInvariants;

namespace Places.Infra;

public static class AirportBuilder
{
    public static Airport? Build(AirportDto dto)
    {
        if (dto.IcaoCode == null || !dto.IcaoCode.DontMatchPattern(IcaoCodePattern))
            return default;

        if (dto.IataCode == null || !dto.IataCode.DontMatchPattern(IataCodePattern))
            return default;

        if (dto.Name.IsNullOrWhiteSpace())
            return default;

        var type = dto.Type.MapToDomain();
        if (type == null)
            return default;

        var continent = dto.Continent.MapToDomain();
        if (continent == null)
            return default;

        var service = dto.ScheduledService.MapToDomain();
        if (service == null)
            return default;

        if (dto.Longitude == null || Math.Abs(dto.Longitude.Value) > 180)
            return default;

        if (dto.Latitude == null || Math.Abs(dto.Latitude.Value) > 90)
            return default;

        return new Airport
        {
            Id = dto.Id,
            IcaoCode = dto.IcaoCode,
            IataCode = dto.IataCode,
            Type = type.Value,
            Continent = continent.Value,
            ScheduledService = service.Value,
            Name = dto.Name,
            Location = new Location(dto.Longitude.Value, dto.Latitude.Value)
        };
    }
}