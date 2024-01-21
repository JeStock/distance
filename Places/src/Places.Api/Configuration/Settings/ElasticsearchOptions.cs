namespace Places.Api.Configuration.Settings;

public class ElasticsearchOptions
{
    public required string DataUrl { get; init; }
    public required string LogUrl { get; init; }
}