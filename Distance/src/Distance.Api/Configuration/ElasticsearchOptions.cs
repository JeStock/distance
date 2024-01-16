namespace Distance.Api.Configuration;

public class ElasticsearchOptions
{
    public required string DataUrl { get; init; }
    public required string LogUrl { get; init; }
}