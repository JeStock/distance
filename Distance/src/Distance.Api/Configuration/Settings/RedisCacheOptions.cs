namespace Distance.Api.Configuration.Settings;

public class RedisCacheOptions
{
    public required bool UseCache { get; init; }
    public required string ConnectionString { get; init; }
}