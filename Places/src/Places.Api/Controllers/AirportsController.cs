using System.ComponentModel.DataAnnotations;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Places.Api.Models.Responses;
using Places.Core.Contracts.Services;
using Places.Core.Domain;
using Swashbuckle.AspNetCore.Annotations;
using static Places.Api.ApiHelpers;

namespace Places.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportsController(IAirportsService airportsService) : ControllerBase
{
    [HttpGet("{iata}")]
    [SwaggerOperation(Summary = "Returns airport data by specified IATA code")]
    [ProducesResponseType<AirportResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<BadRequestResult>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlaceByIataAsync(
        [Required, RegularExpression(Iata.CodePattern),
         SwaggerParameter("Airport's IATA (3 upper case letters). E.g. AMS, BCN, MAD, CDG, FRA, BER")]
        string iata, CancellationToken token = default)
    {
        var iataParsingResult = Iata.Parse(iata);
        if (iataParsingResult.IsFailure)
            return BadRequest(iataParsingResult.Error);

        return await airportsService
            .GetByIataAsync(iataParsingResult.Value, token)
            .Match(
                onSuccess: OkResponse,
                onFailure: NotFoundResponse
            );
    }
}