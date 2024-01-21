using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Infra.Clients;

namespace Distance.Infra.Providers;

public class AirportsProvider(IPlacesRestApi client) : IAirportsProvider
{
    public async Task<Maybe<AirportDto>> GetAirportByIataAsync(string iata, CancellationToken token = default)
    {
        var response = await client.GetAirportByIataAsync(iata, token);
        return response.ResponseMessage.IsSuccessStatusCode
            ? response.GetContent()
            : Maybe<AirportDto>.None;
    }
}