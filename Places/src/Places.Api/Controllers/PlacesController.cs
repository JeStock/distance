using Elastic.Transport;
using Microsoft.AspNetCore.Mvc;
using Places.Infra.Elastic.Factories;

namespace Places.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacesController : ControllerBase
{
    // TODO [sg]: replaces with service
    private readonly IElasticClientFactory elasticFactory;

    public PlacesController(IElasticClientFactory elasticFactory)
    {
        this.elasticFactory = elasticFactory;
    }

    [HttpGet("/{iata}")]
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType<NotFoundResult>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPlaceByIataAsync(string iata, CancellationToken token = default)
    {
        var elastic = elasticFactory.GetClient();
        var model = new TestModel
        {
            Id = 534, Message = "Hello Elastic"
        };

        var indexResponse = await elastic.IndexAsync(model, "test-model-index", token);
        if (indexResponse.IsValidResponse)
            Console.WriteLine("TestMessage indexed successfully!");

        var response = await elastic.GetAsync<TestModel>(534, idx => idx.Index("test-model-index"), token);
        if(response.IsValidResponse)
            Console.WriteLine("Indexed TestMessage retrieved successfully!");

        return Ok($"Places responded '{iata}'");
    }
}