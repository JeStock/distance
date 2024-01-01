using System.Globalization;
using System.Reflection;
using CsvHelper;
using Places.DataSeeder.Csv.Mappings;
using Places.DataSeeder.Models;

namespace Places.DataSeeder.Csv;

// TODO [sg]: comment on Dispose Pattern
public class AirportsHandler : IAirportsHandler
{
    private StreamReader? _streamReader;
    private CsvReader? _csvReader;
    private bool _isAlreadyDisposed;

    public IAsyncEnumerable<AirportDto> GetAirportsAsync(CancellationToken token)
    {
        var projectRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        _streamReader = new StreamReader($"""{projectRootPath}\airports.csv""");
        _csvReader = new CsvReader(_streamReader, CultureInfo.InvariantCulture);

        _csvReader.Context.RegisterClassMap<AirportDtoMap>();

        return _csvReader.GetRecordsAsync<AirportDto>(token);
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (_isAlreadyDisposed) return;

        if (isDisposing)
        {
            _streamReader?.Dispose();
            _csvReader?.Dispose();
        }

        _isAlreadyDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~AirportsHandler() => Dispose(false);
}