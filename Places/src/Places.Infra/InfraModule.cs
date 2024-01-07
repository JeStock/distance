using System.Collections.Frozen;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Places.Domain;
using Places.Domain.Enums;
using Places.Infra.Configuration;
using Places.Infra.Elastic;
using Places.Infra.Elastic.Factories;
using Places.Shared;

namespace Places.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfraModule(this IServiceCollection services, IConfiguration config)
    {
        return services.AddElastic(config);
    }

    private static IServiceCollection AddElastic(this IServiceCollection services, IConfiguration config)
    {
        var elasticConfig = config.GetSection<Elasticsearch>();
        if (elasticConfig == null)
            throw new NotImplementedException(); // TODO [sg]: Introduce ServiceException and use it here

        var elasticConnectionSettings = new ElasticsearchClientSettings(
            new StaticNodePool(
                elasticConfig.Urls.Split(',').Select(s => new Uri(s)).ToArray()
            )
        );

        services.AddSingleton<FrozenDictionary<int, Continent>>(_ =>
            EnumMapConverter.Enumerate<Continent>());

        services.AddSingleton(new ElasticsearchClient(elasticConnectionSettings));
        services.AddSingleton<IElasticClientFactory, ElasticClientFactory>();
        services.AddSingleton<IAirportIndexFacade, AirportIndexFacade>();

        return services;
    }
}