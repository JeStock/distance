using Microsoft.Extensions.Hosting;
using Places.DataSeeder.Csv;
using Places.DataSeeder.Models;
using Places.Infra.Factories;

namespace Places.DataSeeder;

public class DataSeedBackgroundWorker : IHostedService, IDisposable
{
    private readonly IElasticClientFactory _factory;
    private readonly IAirportsHandler _airportsHandler;

    public DataSeedBackgroundWorker(IElasticClientFactory factory, IAirportsHandler airportsHandler)
    {
        _factory = factory;
        _airportsHandler = airportsHandler;
    }

    public async Task StartAsync(CancellationToken token)
    {
        await ProcessCsvAsync(token);
    }

    // TODO [sg]: add logging
    public Task StopAsync(CancellationToken token)
    {
        return Task.CompletedTask;
    }

    public async Task ProcessCsvAsync(CancellationToken token)
    {
        var list = new List<AirportDto>();
        var records = _airportsHandler.GetAirportsAsync(token);
        await foreach (var record in records)
        {
            list.Add(record);
            // TODO [sg]: add model processing and mapping
            //await client.IndexAsync(record, token);
        }

        var types = list.Select(x => x.Type).Distinct();
        var continents = list.Select(x => x.Continent).Distinct();
        var scheduledServices = list.Select(x => x.ScheduledService).Distinct();

        Print("Types", types);
        Print("Continents", continents);
        Print("ScheduledServices", scheduledServices);
    }

    private static void Print<T>(string name, IEnumerable<T?> items)
        => Console.WriteLine($"{name}: {string.Join(", ", items)}");

    public void Dispose() => _airportsHandler.Dispose();
}