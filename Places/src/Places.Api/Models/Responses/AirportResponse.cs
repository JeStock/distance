using Places.Core.Contracts.Models;
using Places.Core.Domain;
using Swashbuckle.AspNetCore.Annotations;

namespace Places.Api.Models.Responses;

public class AirportResponse
{
    [SwaggerSchema("Airport's unique identifier")]
    public required int Id { get; init; }

    [SwaggerSchema("Airport's name")]
    public required string Name { get; init; }

    [SwaggerSchema("ICAO code. (4 upper case letters)")]
    public required string IcaoCode { get; init; }

    [SwaggerSchema("IATA code. (3 upper case letters)")]
    public required string IataCode { get; init; }

    [SwaggerSchema("Airport's coordinates")]
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