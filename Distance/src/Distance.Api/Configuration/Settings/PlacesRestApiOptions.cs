namespace Distance.Api.Configuration.Settings;

public class PlacesRestApiOptions
{
    public required string Url { get; init; }
    public required int RetryCount { get; init; }
    public required int BackoffPower { get; init; }
}