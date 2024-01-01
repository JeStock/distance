using Places.DataSeeder.Models;

namespace Places.DataSeeder.Csv;

public interface IAirportsHandler : IDisposable
{
    IAsyncEnumerable<AirportDto> GetAirportsAsync(CancellationToken token);
}