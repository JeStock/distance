using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Infra.Clients;

namespace Distance.Infra.Providers;

public class AirportsProvider(IPlacesRestApi client) : IAirportsProvider
{
    public async Task<Maybe<AirportDto>> GetAsync(string iata, CancellationToken token)
    {
        var response = await client.GetAirportByIataAsync(iata, token);
        return response.GetContent();
        /*return new AirportDto
        {
            Id = 1,
            Name = "name",
            IcaoCode = "icao",
            IataCode = "iata",
            Type = "type",
            Continent = "content",
            ScheduledService = "yes",
            Location = new LocationDto
            {
                Latitude = 30,
                Longitude = 30
            }
        };*/
    }
}