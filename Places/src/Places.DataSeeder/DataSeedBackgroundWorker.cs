using System.Globalization;
using System.Reflection;
using CsvHelper;
using Microsoft.Extensions.Hosting;
using Places.Infra.Factories;

namespace Places.DataSeeder;

public class DataSeedBackgroundWorker : IHostedService
{
    private readonly IElasticClientFactory _factory;

    public DataSeedBackgroundWorker(IElasticClientFactory factory)
    {
        _factory = factory;
    }

    public async Task StartAsync(CancellationToken token)
    {
        await ProcessCsvAsync(token);
    }

    public Task StopAsync(CancellationToken token)
    {
        return Task.CompletedTask;
    }

    public async Task ProcessCsvAsync(CancellationToken token)
    {
        var row = new AirportDto();
        //var client = _factory.GetClient();
        var projectRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        using var reader = new StreamReader($"""{projectRootPath}\airports.csv""");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<AirportDtoMap>();

        var records = csv.EnumerateRecordsAsync(row, token);
        await foreach (var record in records)
        {
            // TODO [sg]: add model processing and mapping
            //await client.IndexAsync(record, token);
        }
    }
}