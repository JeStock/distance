using Microsoft.Extensions.Configuration;

namespace Places.Shared;

public static class ConfigurationExtensions
{
    public static T? GetSection<T>(this IConfiguration config) =>
        config.GetSection(typeof(T).Name).Get<T>();
}