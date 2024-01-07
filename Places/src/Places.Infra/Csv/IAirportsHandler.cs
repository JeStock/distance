using Places.Infra.Csv.Models;

namespace Places.Infra.Csv;

public interface IAirportsHandler : IDisposable
{
    IAsyncEnumerable<AirportDto> GetAirportsAsync(CancellationToken token = default);
}