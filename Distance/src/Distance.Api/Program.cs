using Distance.PlacesClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPlacesClientModule(builder.Configuration);

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("api/{iata}", async (string iata, IPlacesRestApi placesApi) =>
    {
        var response = await placesApi.GetPlaceAsync(iata, default);
        return response.ResponseMessage.IsSuccessStatusCode
            ? $"Hello from Distance & Places: {response.StringContent}!"
            : $"Hello from Distance: {iata}!";
    })
    .WithName("Calculate the distance between two airports")
    .WithOpenApi();

app.Run();