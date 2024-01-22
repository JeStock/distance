using Elastic.Clients.Elasticsearch;
using Places.Api.Configuration;
using Places.Api.Configuration.Settings;
using Places.Core.Contracts.Elastic;
using Places.Infra.Elastic;
using AirportsRepository = Places.Infra.Elastic.AirportsRepository;

namespace Places.Api.Composition;

public static class InfrastructureModule
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
        services.AddSingleton<IAirportsRepository, AirportsRepository>();

        return services;
    }
}