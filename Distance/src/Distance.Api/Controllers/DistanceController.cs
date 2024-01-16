using Distance.Api.Models;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Services;
using Distance.Infra.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Distance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistanceController(
    IDistanceService service,
    IPlacesRestApi placesApi) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BadRequestResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlaceByIataAsync(
        [FromQuery] DistanceQuery query,
        CancellationToken token = default)
    {
        var itinerary = new ItineraryDto(query.Origin, query.Destination);
        var distance = service.GetDistanceAsync(itinerary, token);

        var originTask = placesApi.GetAirportByIataAsync(query.Origin, token);
        var destinationTask = placesApi.GetAirportByIataAsync(query.Destination, token);

        await Task.WhenAll(originTask, destinationTask);

        var origin = originTask.Result;
        var destination = destinationTask.Result;

        if (origin.ResponseMessage.IsSuccessStatusCode && destination.ResponseMessage.IsSuccessStatusCode)
        {
            var originLocation = origin.GetContent().Location;
            var originCoordinate = new Geolocation.Coordinate(
                originLocation.Latitude,
                originLocation.Longitude);

            var destinationLocation = destination.GetContent().Location;
            var destinationCoordinate = new Geolocation.Coordinate(
                destinationLocation.Latitude,
                destinationLocation.Longitude);

            return Ok(Geolocation.GeoCalculator.GetDistance(originCoordinate, destinationCoordinate));
        }

        return BadRequest();
    }
}