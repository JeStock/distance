using Distance.Api.Configuration;
using Distance.Api.Configuration.Settings;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Infra.Clients;
using Distance.Infra.Providers;
using Distance.Infra.Repositories;
using Polly;
using RestEase.HttpClientFactory;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Distance.Api.Composition;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration config)
    {
        var placesApiConfig = config.GetSection<PlacesRestApiOptions>();
        if (placesApiConfig == null)
            throw new ServiceConfigurationException($"{nameof(PlacesRestApiOptions)} are required");

        services.AddRestEaseClient<IPlacesRestApi>(placesApiConfig.Url)
            .AddTransientHttpErrorPolicy(policyBuilder =>
                policyBuilder.WaitAndRetryAsync(placesApiConfig.RetryCount,
                    retryAttempt => TimeSpan.FromSeconds(
                        Math.Pow(placesApiConfig.BackoffPower, retryAttempt)
                    )
                )
            );

        services.AddScoped<IAirportsProvider, AirportsProvider>();

        var redisConf = config.GetSection<RedisCacheOptions>();
        if (redisConf?.UseCache == true)
        {
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(new RedisConfiguration
            {
                ConnectionString = redisConf.ConnectionString
            });

            services.AddScoped<IRepository<AirportDto>, AirportsRepository>();
            services.Decorate<IAirportsProvider, AirportsProviderCacheDecorator>();
        }

        return services;
    }
}