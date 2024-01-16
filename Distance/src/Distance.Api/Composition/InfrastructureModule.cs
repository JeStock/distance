using Distance.Api.Configuration;
using Distance.Infra.Clients;
using Polly;
using RestEase.HttpClientFactory;

namespace Distance.Api.Composition;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration config)
    {
        var placesApiConfig = config.GetSection<PlacesRestApiOptions>();
        if (placesApiConfig == null)
            throw new Exception("PlacesApi Url must be set"); // TODO [sg]: configuration exception.

        services.AddRestEaseClient<IPlacesRestApi>(placesApiConfig.Url)
            .AddTransientHttpErrorPolicy(policyBuilder =>
                policyBuilder.WaitAndRetryAsync(placesApiConfig.RetryCount,
                    retryAttempt => TimeSpan.FromSeconds(
                        Math.Pow(placesApiConfig.BackoffPower, retryAttempt)
                    )
                )
            );

        return services;
    }
}