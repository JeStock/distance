using Distance.Core.Contracts.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Distance.Api;

public static class ApiHelpers
{
    public static IActionResult OkResponse(Core.Domain.Distance dto) =>
        new OkObjectResult(DistanceResponse.FromDomain(dto));
}