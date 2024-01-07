using Microsoft.Extensions.Hosting;
using Places.Infra;
using Places.Infra.Csv;
using Places.Infra.Elastic;
using Places.Infra.Elastic.Models;
using Places.Shared;

namespace Places.DataSeeder;

public class DataSeedBackgroundWorker(
    IAirportsHandler airportsHandler,
    IAirportIndexFacade airportIndexFacade)
    : IHostedService, IDisposable
{
    public async Task StartAsync(CancellationToken token)
    {
        var indexCreated = await airportIndexFacade.CreateAirportsIndexAsync(token);
        if (indexCreated == OperationResult.Failure) // TODO [sg]: add log
            return;

        var recordBatches = airportsHandler.GetAirportsAsync(token).ProcessBatch(50, token);
        await foreach (var batch in recordBatches)
        {
            var airports = batch.Select(AirportBuilder.Build)
                .Where(x => x != null)
                .Select(x => x!);

            // TODO [sg]: add log bach processed
            await airportIndexFacade.BulkIndexAirportsAsync(airports, token);
        }
    }

    public Task StopAsync(CancellationToken token)
    {
        return airportIndexFacade.DeleteAirportsIndexAsync(token);
    }

    public void Dispose() => airportsHandler.Dispose();
}