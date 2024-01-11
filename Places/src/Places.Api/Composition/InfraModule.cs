using Elastic.Clients.Elasticsearch;
using Places.Api.Configuration;
using Places.Core.Contracts.Elastic;
using Places.Infra.Elastic;
using Places.Shared;

namespace Places.Api.Composition;

public static class InfraModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services, IConfiguration config)
    {
        var elasticConfig = config.GetSection<ElasticsearchOptions>();
        if (elasticConfig == null)
            throw new ServiceConfigurationException($"{nameof(ElasticsearchOptions)} are required");

        var elasticConnectionSettings = new ElasticsearchClientSettings(new Uri(elasticConfig.DataUrl));
        services.AddSingleton(new ElasticsearchClient(elasticConnectionSettings));

        services.AddSingleton<IElasticClientFactory, ElasticClientFactory>();
        services.AddSingleton<IAirportsIndexFacade, AirportsIndexFacade>();

        return services;
    }
}