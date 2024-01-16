using Distance.Core.Contracts.Models;
using RestEase;

namespace Distance.Infra.Clients;

[Header("Accept", "application/json")]
public interface IPlacesRestApi
{
    [Get("api/airports/{iata}")]
    Task<Response<AirportDto>> GetAirportByIataAsync([Path] string iata, CancellationToken token);
}