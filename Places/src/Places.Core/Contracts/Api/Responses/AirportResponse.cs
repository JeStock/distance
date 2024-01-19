using Places.Core.Contracts.Models;
using Places.Core.Domain;

namespace Places.Core.Contracts.Api.Responses;

public class AirportResponse
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string IcaoCode { get; init; }
    public required string IataCode { get; init; }
    public required LocationDto Location { get; init; }

    public static AirportResponse FromDomain(Airport domain) =>
        new()
        {
            Id = domain.Id,
            Name = domain.Name,
            IcaoCode = domain.Icao.Code,
            IataCode = domain.Iata.Code,
            Location = domain.Location.ToDto()
        };
}