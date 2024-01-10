using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Places.Core.Contracts.Elastic;
using Places.Core.Contracts.Models;
using Places.Core.Domain;

namespace Places.Infra.Elastic;

public class AirportsIndexFacade(IElasticClientFactory factory) : IAirportsIndexFacade
{
    private const string AirportsIndexName = "airports";
    private readonly Indices airportsIndex = Indices.Parse(AirportsIndexName);

    public async Task<OperationResult> CreateAirportsIndexAsync(CancellationToken token = default)
    {
        var indices = factory.GetClient().Indices;

        var deleteIndexResponse = await DeleteAirportsIndexAsync(token);
        if (deleteIndexResponse == OperationResult.Failure)
            return OperationResult.Failure;

        // TODO [sg]: add manual mapping settings
        var indexCreated = await indices
            .CreateAsync<Airport>(x => x.Index(AirportsIndexName), token);

        return ToResult(indexCreated.IsSuccess());
    }

    public async Task<OperationResult> DeleteAirportsIndexAsync(CancellationToken token = default)
    {
        var indices = factory.GetClient().Indices;

        var indexExistsResponse = await indices.ExistsAsync(airportsIndex, token);
        if (!indexExistsResponse.Exists)
            return OperationResult.Success;

        var indexDeleted = await indices.DeleteAsync(airportsIndex, token);

        return ToResult(indexDeleted.IsSuccess());
    }

    public async Task<OperationResult> IndexAirportAsync(AirportDto airport, CancellationToken token = default)
    {
        var elastic = factory.GetClient();

        var indexResponse = await elastic.IndexAsync(airport, AirportsIndexName, token);
        return ToResult(indexResponse.IsSuccess());
    }

    public async Task<OperationResult> BulkIndexAirportsAsync(
        IEnumerable<AirportDto> airports, CancellationToken token = default)
    {
        var elastic = factory.GetClient();
        var bulkRequest = new BulkRequest(AirportsIndexName);

        var indexOps = airports
            .Select(airport => new BulkIndexOperation<AirportDto>(airport))
            .Cast<IBulkOperation>()
            .ToList();

        bulkRequest.Operations = new BulkOperationsCollection(indexOps);
        var bulkIndexResponse = await elastic.BulkAsync(bulkRequest, token);

        var result = bulkIndexResponse.IsSuccess();
        return ToResult(result);
    }

    private static OperationResult ToResult(bool result) => result
        ? OperationResult.Success
        : OperationResult.Failure;
}