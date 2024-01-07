using Places.Api;
using Places.Api.Configuration;
using Places.DataSeeder;
using Places.Infra;
using Places.Infra.Elastic.Factories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfraModule(builder.Configuration)
    .AddDataSeederModule(builder.Configuration);

builder.Services.AddControllers(options => options.AddRoutesConventions());
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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