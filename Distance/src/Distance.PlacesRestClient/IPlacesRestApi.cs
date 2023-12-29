using RestEase;

namespace Distance.PlacesClient;

[Header("Accept", "application/json")]
public interface IPlacesRestApi
{
    [Get("/{iata}")]
    Task<Response<string>> GetPlaceAsync([Path] string iata, CancellationToken token);
}