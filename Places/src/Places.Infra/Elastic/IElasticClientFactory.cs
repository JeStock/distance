using Elastic.Clients.Elasticsearch;

namespace Places.Infra.Elastic;

public interface IElasticClientFactory
{
    ElasticsearchClient GetClient();
}