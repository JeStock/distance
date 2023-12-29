namespace Distance.PlacesClient.Configuration;

public class PlacesRestApi
{
    public required string Url { get; init; }
    public required int RetryCount { get; init; }
    public required int BackoffPower { get; init; }
}