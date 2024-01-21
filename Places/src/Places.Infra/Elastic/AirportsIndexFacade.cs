using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.Core.Bulk;
using Microsoft.Extensions.Logging;
using Places.Core;
using Places.Core.Contracts.Elastic;
using Places.Core.Contracts.Models;
using Places.Core.Domain;
using static Places.Infra.Elastic.IndexNames;

namespace Places.Infra.Elastic;

public class AirportsIndexFacade(IElasticClientFactory factory, ILogger<AirportsIndexFacade> logger) : IAirportsIndexFacade
{
    private readonly Indices airportsIndex = Indices.Parse(AirportsIndexName);

    public async Task<OperationResult> CreateAirportsIndexAsync(CancellationToken token = default)
    {
        var indices = factory.GetClient().Indices;

        var indexExistsResponse = await indices.ExistsAsync(airportsIndex, token);
        if (indexExistsResponse.Exists)
            return OperationResult.Success;

        var indexCreated = await indices
            .CreateAsync<Airport>(x => x.Index(AirportsIndexName), token);

        if (!indexCreated.IsSuccess())
            logger.LogError(indexCreated.ApiCallDetails.OriginalException, "Failed to create index");

        return ToResult(indexCreated.IsSuccess());
    }

    public async Task<OperationResult> DeleteAirportsIndexAsync(CancellationToken token = default)
    {
        var indices = factory.GetClient().Indices;

        var indexExistsResponse = await indices.ExistsAsync(airportsIndex, token);
        if (!indexExistsResponse.Exists)
            return OperationResult.Success;

        var indexDeleted = await indices.DeleteAsync(airportsIndex, token);
        if (!indexDeleted.IsSuccess())
            logger.LogError(indexDeleted.ApiCallDetails.OriginalException, "Failed to delete index");

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