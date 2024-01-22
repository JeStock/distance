using Distance.Api.Models.Responses;
using Distance.Core.Domain;
using Distance.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Distance.Api;

public static class ApiHelpers
{
    public static IActionResult OkResponse(ItineraryDistance dto) =>
        new OkObjectResult(DistanceResponse.FromDomain(dto));

    public static IActionResult NotFoundResponse(string errors) =>
        new NotFoundObjectResult(errors.Split(ErrorHandling.ErrorSeparator));
}