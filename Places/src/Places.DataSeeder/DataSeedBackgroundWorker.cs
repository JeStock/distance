using Microsoft.Extensions.Hosting;
using Places.Core.Contracts.Elastic;
using Places.Core.Domain;
using Places.Shared;
using IAirportsRepository = Places.Core.Contracts.Csv.IAirportsRepository;

namespace Places.DataSeeder;

public class DataSeedBackgroundWorker(
    IAirportsRepository airportsCsvRepository,
    IAirportsIndexFacade airportsIndexFacade)
    : IHostedService, IDisposable
{
    public async Task StartAsync(CancellationToken token)
    {
        var indexCreated = await airportsIndexFacade.CreateAirportsIndexAsync(token);
        if (indexCreated == OperationResult.Failure) // TODO [sg]: add log
            return;

        var recordBatches = airportsCsvRepository.GetAirportsAsync(token).ProcessBatch(50, token);
        await foreach (var batch in recordBatches)
        {
            var airports = batch.Select(Airport.Create)
                .Where(x => x != null)
                .Select(x => x!)
                .ToList();

            // TODO [sg]: add log bach processed
            await airportsIndexFacade.BulkIndexAirportsAsync(airports.Select(x => x.ToDto()), token);
        }
    }

    public Task StopAsync(CancellationToken token)
    {
        return airportsIndexFacade.DeleteAirportsIndexAsync(token);
    }

    public void Dispose() => airportsCsvRepository.Dispose();
}