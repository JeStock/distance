using Places.Core.Contracts.Models;

namespace Places.Core.Contracts.Elastic;

public interface IAirportsIndexFacade
{
    Task<OperationResult> BulkIndexAirportsAsync(IEnumerable<AirportDto> airports, CancellationToken token = default);
    Task<OperationResult> CreateAirportsIndexAsync(CancellationToken token = default);
    Task<OperationResult> DeleteAirportsIndexAsync(CancellationToken token = default);
}