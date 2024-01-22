using Elastic.Clients.Elasticsearch;

namespace Places.Infra.Elastic;

public class ElasticClientFactory(ElasticsearchClient client) : IElasticClientFactory
{
    public ElasticsearchClient GetClient() => client;
}