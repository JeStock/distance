using Elastic.Clients.Elasticsearch;

namespace Places.Infra.Elastic.Factories;

public class ElasticClientFactory(ElasticsearchClient client) : IElasticClientFactory
{
    public ElasticsearchClient GetClient() => client;
}