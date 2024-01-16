namespace Distance.Api.Configuration;

public static class ConfigurationExtensions
{
    public static T? GetSection<T>(this IConfiguration config) =>
        config.GetSection(typeof(T).Name).Get<T>();
}