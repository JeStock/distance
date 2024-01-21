using Distance.Core.Contracts.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Distance.Api;

public static class ApiHelpers
{
    public static IActionResult OkResponse(Core.Domain.ItineraryDistance dto) =>
        new OkObjectResult(DistanceResponse.FromDomain(dto));
}