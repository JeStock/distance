using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Places.Core.Contracts.Services;
using Places.Core.Domain;
using Airport = Places.Core.Domain.Airport;
using static Places.Api.Models.Responses.AirportResponseExtensions;

namespace Places.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportsController(IAirportsService airportsService) : ControllerBase
{
    [HttpGet("{iata}")]
    [ProducesResponseType<Airport>(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BadRequestResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlaceByIataAsync(
        [Required, RegularExpression(Iata.CodePattern)]
        string iata, CancellationToken token = default)
    {
        var iataParsingResult = Iata.Parse(iata);
        if (iataParsingResult.IsFailure)
            return BadRequest(iataParsingResult.Error);

        return await airportsService
            .GetByIataAsync(iataParsingResult.Value, token)
            .Match(
                onSuccess: OkResponse,
                onFailure: NotFound
            );
    }
}