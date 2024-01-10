using Elastic.Transport;
using Microsoft.AspNetCore.Mvc;
using Places.Infra.Elastic;
using HttpMethod = Elastic.Transport.HttpMethod;

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

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello Places!");
    }

    [HttpGet("{iata}")]
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
    
    private async Task TestElasticRestApi(CancellationToken token)
    {
        var elastic = elasticFactory.GetClient();
        var body = """
                   {
                     "mappings": {
                       "properties": {
                         "age":    { "type": "integer" },
                         "email":  { "type": "keyword"  },
                         "name":   { "type": "text"  }
                       }
                     }
                   }
                   """;
        var resp = await elastic.Transport
            .RequestAsync<StringResponse>(HttpMethod.PUT, "/test-elastic-rest-api", PostData.String(body), token);

        var resp1 = resp.Body;
    }
}