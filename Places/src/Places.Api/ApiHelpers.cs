using Microsoft.AspNetCore.Mvc;
using Places.Api.Models.Responses;
using Places.Core.Domain;
using Places.Shared;

namespace Places.Api;

public class ApiHelpers
{
    public static IActionResult OkResponse(Airport domain) =>
        new OkObjectResult(AirportResponse.FromDomain(domain));
    
    public static IActionResult NotFoundResponse(string errors) =>
        new NotFoundObjectResult(errors.Split(ErrorHandling.ErrorSeparator));
}