using CSharpFunctionalExtensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Places.Core;
using Places.Core.Contracts.Elastic;
using Places.Core.Domain;
using Places.Shared;
using static Places.Shared.ErrorHandling;
using IAirportsRepository = Places.Core.Contracts.Csv.IAirportsRepository;

namespace Places.DataSeeder;

public class DataSeedBackgroundWorker(
    IAirportsRepository airportsCsvRepository,
    IAirportsIndexFacade airportsIndexFacade,
    ILogger<DataSeedBackgroundWorker> logger)
    : IHostedService, IDisposable
{
    public async Task StartAsync(CancellationToken token = default)
    {
        logger.LogInformation("Bootstrapping {Worker}", nameof(DataSeedBackgroundWorker));

        await HandleAsync(token);

        logger.LogInformation("{Worker} have finished the job", nameof(DataSeedBackgroundWorker));
    }

    public Task StopAsync(CancellationToken token = default)
    {
        logger.LogInformation("Shutting down {Worker}", nameof(DataSeedBackgroundWorker));
        return airportsIndexFacade.DeleteAirportsIndexAsync(token);
    }

    private async Task HandleAsync(CancellationToken token = default)
    {
        logger.LogInformation("Importing data from csv file to Elasticsearch");

        // NOTE: for demonstration purposes the index is recreated every time
        var indexDeleted = await airportsIndexFacade.DeleteAirportsIndexAsync(token);
        if (indexDeleted == OperationResult.Failure)
        {
            logger.LogInformation("Failed to delete Airports index");
            return;
        }

        var indexCreated = await airportsIndexFacade.CreateAirportsIndexAsync(token);
        if (indexCreated == OperationResult.Failure)
        {
            logger.LogWarning("Failed to create Airports index");
            return;
        }

        logger.LogInformation("Airports index successfully created");

        const int batchSize = 100;
        var totalCount = 0;
        var indexedCount = 0;
        var skippedCount = 0;
        var errorMessages = new List<string>();

        var batchParseErrors = new List<string>(batchSize);
        var batchParsedAirports = new List<Airport>(batchSize);

        var recordBatches = airportsCsvRepository.GetAirportsAsync(token).ProcessBatch(batchSize, token);
        await foreach (var batch in recordBatches)
        {
            totalCount += batch.Length;

            batchParseErrors.Clear();
            batchParsedAirports.Clear();

            foreach (var airportDto in batch)
            {
                var parsingResult = Airport.Parse(airportDto);
                parsingResult.Match(
                    airport => batchParsedAirports.Add(airport),
                    parseError =>
                    {
                        // TODO [sg]: index error reports
                        batchParseErrors.Add(parseError);
                    });
            }

            skippedCount += batchParseErrors.Count;
            errorMessages.AddRange(batchParseErrors.SelectMany(x => x.Split(ErrorSeparator)));

            await airportsIndexFacade.BulkIndexAirportsAsync(batchParsedAirports.Select(x => x.ToDto()), token);
            indexedCount += batchParsedAirports.Count;
        }

        logger.LogInformation("Data import completed");

        logger.LogInformation("{ImportedCount} out of {TotalCount} entries have been successfully imported",
            indexedCount, totalCount);

        logger.LogInformation("{SkippedCount} out of {TotalCount} entries failed validation and have been skipped",
            skippedCount, totalCount);

        logger.LogInformation("Statistics (one record could contain several errors):");
        foreach (var errorMessage in errorMessages.GroupBy(x => x))
            logger.LogInformation("'{ErrorType}': {Count}", errorMessage.Key, errorMessage.Count());
    }

    public void Dispose() => airportsCsvRepository.Dispose();
}