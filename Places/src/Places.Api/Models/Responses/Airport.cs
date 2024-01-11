using Microsoft.AspNetCore.Mvc;
using Places.Core.Contracts.Models;
using AirportDomain = Places.Core.Domain.Airport;

namespace Places.Api.Models.Responses;

public class Airport
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string IcaoCode { get; init; }
    public required string IataCode { get; init; }
    public required string Type { get; init; }
    public required string Continent { get; init; }
    public required string ScheduledService { get; init; }
    public required LocationDto Location { get; init; } = new();
}

public static class AirportResponseExtensions
{
    public static IActionResult OkResponse(AirportDomain domain) =>
        new OkObjectResult(new Airport
        {
            Id = domain.Id,
            Name = domain.Name,
            IcaoCode = domain.Icao.Code,
            IataCode = domain.Iata.Code,
            Type = domain.Type.ToString(),
            Continent = domain.Continent.ToString(),
            ScheduledService = domain.ScheduledService.ToString(),
            Location = domain.Location.ToDto()
        });
}