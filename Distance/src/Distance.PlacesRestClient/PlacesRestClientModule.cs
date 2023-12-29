using Distance.PlacesClient.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using RestEase.HttpClientFactory;

namespace Distance.PlacesClient;

public static class PlacesRestClientModule
{
    public static IServiceCollection AddPlacesClientModule(this IServiceCollection services, IConfiguration config)
    {
        var placesApiConfig = config.GetSection<PlacesRestApi>();
        if (placesApiConfig == null)
            throw new Exception("PlacesApi Url must be set"); // TODO [sg]: configuration exception.

        services.AddRestEaseClient<IPlacesRestApi>(placesApiConfig.Url)
            .AddTransientHttpErrorPolicy(policyBuilder =>
                policyBuilder.WaitAndRetryAsync(placesApiConfig.RetryCount,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(placesApiConfig.BackoffPower, retryAttempt))));

        return services;
    }

    private static T? GetSection<T>(this IConfiguration config) =>
        config.GetSection(typeof(T).Name).Get<T>();
}