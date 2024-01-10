using Places.Core.Contracts.Models;
using Places.Core.Domain;

namespace Places.Core.Contracts.Elastic;

public interface IAirportsIndexFacade
{
    Task<OperationResult> IndexAirportAsync(AirportDto airport, CancellationToken token = default);
    Task<OperationResult> BulkIndexAirportsAsync(IEnumerable<AirportDto> airports, CancellationToken token = default);
    Task<OperationResult> CreateAirportsIndexAsync(CancellationToken token = default);
    Task<OperationResult> DeleteAirportsIndexAsync(CancellationToken token = default);
}