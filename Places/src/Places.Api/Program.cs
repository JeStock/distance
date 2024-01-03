using Places.DataSeeder;
using Places.Infra;
using Places.Infra.Factories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfraModule(builder.Configuration)
    .AddDataSeederModule(builder.Configuration);

var app = builder.Build();

app.MapGet("/{iata}", async (string iata, IElasticClientFactory elasticFactory) =>
{
    var elastic = elasticFactory.GetClient();
    var model = new TestModel
    {
        Id = 534, Message = "Hello Elastic"
    };

    var indexResponse = await elastic.IndexAsync(model, "test-model-index");
    if (indexResponse.IsValidResponse)
        Console.WriteLine("TestMessage indexed successfully!");

    var response = await elastic.GetAsync<TestModel>(534, idx => idx.Index("test-model-index"));
    if(response.IsValidResponse)
        Console.WriteLine("Indexed TestMessage retrieved successfully!");

    return $"Places responded '{iata}'";
});

app.Run();

public class TestModel
{
    public required long Id { get; init; }
    public required string Message { get; init; }
}