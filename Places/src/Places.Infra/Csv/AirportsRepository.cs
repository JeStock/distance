using System.Globalization;
using System.Reflection;
using CsvHelper;
using Places.Core.Contracts.Csv;
using Places.Core.Contracts.Models;
using Places.Infra.Csv.Mapping;

namespace Places.Infra.Csv;

public class AirportsRepository : IAirportsRepository
{
    private StreamReader? streamReader;
    private CsvReader? csvReader;
    private bool isAlreadyDisposed;

    public IAsyncEnumerable<AirportDto> GetAirportsAsync(CancellationToken token = default)
    {
        var projectRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var pathToFile = Path.Combine(projectRootPath!, "airports.csv");

        streamReader = new StreamReader(pathToFile);
        csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

        csvReader.Context.RegisterClassMap<AirportDtoMap>();

        return csvReader.GetRecordsAsync<AirportDto>(token);
    }

    private void Dispose(bool isDisposing)
    {
        if (isAlreadyDisposed) return;

        if (isDisposing)
        {
            streamReader?.Dispose();
            csvReader?.Dispose();
        }

        isAlreadyDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AirportsRepository() => Dispose(false);
}