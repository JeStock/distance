using Elastic.Clients.Elasticsearch;

namespace Places.Infra.Elastic.Factories;

public interface IElasticClientFactory
{
    ElasticsearchClient GetClient();
}