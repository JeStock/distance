using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Places.Core.Contracts.Elastic;
using Places.Core.Domain;
using Places.Shared;
using IAirportsRepository = Places.Core.Contracts.Csv.IAirportsRepository;

namespace Places.DataSeeder;

public class DataSeedBackgroundWorker(
    IAirportsRepository airportsCsvRepository,
    IAirportsIndexFacade airportsIndexFacade,
    ILogger<DataSeedBackgroundWorker> logger)
    : IHostedService, IDisposable
{
    public async Task StartAsync(CancellationToken token)
    {
        logger.LogInformation("Importing data from csv file to Elasticsearch");

        var indexCreated = await airportsIndexFacade.CreateAirportsIndexAsync(token);
        if (indexCreated == OperationResult.Failure) // TODO [sg]: add log
            return;

        logger.LogInformation("Airports index successfully created");

        var totalAmount = 0;
        var importedCount = 0;
        var recordBatches = airportsCsvRepository.GetAirportsAsync(token).ProcessBatch(100, token);
        await foreach (var batch in recordBatches)
        {
            totalAmount += batch.Length;

            var airports = batch.Select(Airport.Create)
                .Where(x => x != null)
                .Select(x => x!)
                .ToList();

            importedCount += airports.Count;

            await airportsIndexFacade.BulkIndexAirportsAsync(airports.Select(x => x.ToDto()), token);
        }

        logger.LogInformation("Data import completed. {ImportedCount} out of {TotalAmount} airports have been imported",
            importedCount, totalAmount);
    }

    public Task StopAsync(CancellationToken token)
    {
        logger.LogInformation("Shut down {Worker}", nameof(DataSeedBackgroundWorker));
        return airportsIndexFacade.DeleteAirportsIndexAsync(token);
    }

    public void Dispose() => airportsCsvRepository.Dispose();
}