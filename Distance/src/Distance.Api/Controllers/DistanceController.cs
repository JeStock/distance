using CSharpFunctionalExtensions;
using Distance.Api.Models.Requests;
using Distance.Api.Models.Responses;
using Distance.Core.Contracts.Services;
using Distance.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static Distance.Api.ApiHelpers;

namespace Distance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistanceController(IDistanceService service) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Calculates distance between two airports specified by their IATA codes")]
    [ProducesResponseType<DistanceResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BadRequestResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlaceByIataAsync(
        [FromQuery] DistanceQuery query,
        CancellationToken token = default)
    {
        var itinerary = Itinerary.Parse(query.Origin, query.Destination);
        if (itinerary.IsFailure)
            return BadRequest(itinerary.Error);

        return await service.GetDistanceAsync(itinerary.Value, token)
            .Match(
                onSuccess: OkResponse,
                onFailure: NotFoundResponse
            );
    }
}