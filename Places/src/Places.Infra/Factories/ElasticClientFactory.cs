using Elastic.Clients.Elasticsearch;

namespace Places.Infra.Factories;

public class ElasticClientFactory(ElasticsearchClient client) : IElasticClientFactory
{
    public ElasticsearchClient GetClient() => client;
}