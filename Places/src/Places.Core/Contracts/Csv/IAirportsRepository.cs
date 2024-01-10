using Places.Core.Contracts.Models;

namespace Places.Core.Contracts.Csv;

public interface IAirportsRepository : IDisposable
{
    IAsyncEnumerable<AirportDto> GetAirportsAsync(CancellationToken token = default);
}