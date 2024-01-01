using Elastic.Clients.Elasticsearch;

namespace Places.Infra.Factories;

public interface IElasticClientFactory
{
    ElasticsearchClient GetClient();
}