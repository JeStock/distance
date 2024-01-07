using System.Globalization;
using System.Reflection;
using CsvHelper;
using Places.Infra.Csv.Mapping;
using Places.Infra.Csv.Models;

namespace Places.Infra.Csv;

// TODO [sg]: comment on Dispose Pattern
public sealed class AirportsHandler : IAirportsHandler
{
    private StreamReader? streamReader;
    private CsvReader? csvReader;
    private bool isAlreadyDisposed;

    public IAsyncEnumerable<AirportDto> GetAirportsAsync(CancellationToken token = default)
    {
        var projectRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        streamReader = new StreamReader($"""{projectRootPath}\airports.csv""");
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

    ~AirportsHandler() => Dispose(false);
}