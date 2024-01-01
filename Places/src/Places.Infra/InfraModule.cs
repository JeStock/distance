﻿using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Places.Infra.Configuration;
using Places.Infra.Factories;
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

        services.AddSingleton(new ElasticsearchClient(elasticConnectionSettings));
        services.AddSingleton<IElasticClientFactory, ElasticClientFactory>();

        return services;
    }
}