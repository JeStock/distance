﻿using CSharpFunctionalExtensions;
using Distance.Api.Models;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Services;
using Distance.Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Distance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistanceController(IDistanceService service) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<DistanceDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BadRequestResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlaceByIataAsync(
        [FromQuery] DistanceQuery query,
        CancellationToken token = default)
    {
        if (query.Origin == query.Destination)
            return Ok(new DistanceDto(0));

        var itinerary = Itinerary.Parse(query.Origin, query.Destination);
        if (itinerary.IsFailure)
            return BadRequest(itinerary.Error);

        return await service.GetDistanceAsync(itinerary.Value, token)
            .Match(
                onSuccess: distanceDto => (IActionResult) Ok(distanceDto),
                onFailure: NotFound
            );
    }
}