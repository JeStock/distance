using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Places.Core.Contracts.Api.Responses;
using Places.Core.Contracts.Services;
using Places.Core.Domain;
using static Places.Api.ApiHelpers;

namespace Places.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportsController(IAirportsService airportsService) : ControllerBase
{
    [HttpGet("{iata}")]
    [ProducesResponseType<AirportResponse>(StatusCodes.Status200OK)]
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