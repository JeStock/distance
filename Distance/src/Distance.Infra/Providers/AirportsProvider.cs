﻿using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Core.Domain;
using Distance.Infra.Clients;
using Microsoft.Extensions.Logging;
using static Distance.Shared.ErrorHandling;

namespace Distance.Infra.Providers;

public class AirportsProvider(IPlacesRestApi client, ILogger<AirportsProvider> logger) : IAirportsProvider
{
    public async Task<Maybe<AirportDto>> GetAirportAsync(string iata, CancellationToken token = default)
    {
        try
        {
            var response = await client.GetAirportByIataAsync(iata, token);
            return response.ResponseMessage.IsSuccessStatusCode
                ? response.GetContent()
                : Maybe<AirportDto>.None;
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Failed to get airport data. Places service is unavailable");
            return Maybe<AirportDto>.None;
        }
    }

    public async Task<Result<(AirportDto Origin, AirportDto Destination)>> GetItineraryAirportsAsync(
        Itinerary itinerary, CancellationToken token = default)
    {
        var originTask = GetAirportAsync(itinerary.Origin.Code, token);
        var destinationTask = GetAirportAsync(itinerary.Destination.Code, token);

        await Task.WhenAll(originTask, destinationTask);

        var originAirport = await originTask.ToResult($"{itinerary.Origin.Code} airport not found");
        var destinationAirport = await destinationTask.ToResult($"{itinerary.Destination.Code} airport not found");
        var airportsCombinedResult = Combine(originAirport, destinationAirport);

        return airportsCombinedResult.IsFailure
            ? FailWith<(AirportDto, AirportDto)>(airportsCombinedResult.Error)
            : (originAirport.Value, destinationAirport.Value);
    }
}