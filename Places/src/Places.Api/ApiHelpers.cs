using Microsoft.AspNetCore.Mvc;
using Places.Core.Domain;
using Places.Core.Contracts.Api.Responses;

namespace Places.Api;

public class ApiHelpers
{
    public static IActionResult OkResponse(Airport domain) =>
        new OkObjectResult(AirportResponse.FromDomain(domain));
}