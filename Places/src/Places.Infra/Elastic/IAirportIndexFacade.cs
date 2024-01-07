using Elastic.Clients.Elasticsearch;
using Places.Domain.Models;
using Places.Infra.Elastic.Models;

namespace Places.Infra.Elastic;

public interface IAirportIndexFacade
{
    ElasticsearchClient GetClient();
    Task<OperationResult> IndexAirportAsync(Airport airport, CancellationToken token = default);
    Task<OperationResult> BulkIndexAirportsAsync(IEnumerable<Airport> airports, CancellationToken token = default);
    Task<OperationResult> CreateAirportsIndexAsync(CancellationToken token = default);
    Task<OperationResult> DeleteAirportsIndexAsync(CancellationToken token = default);
}