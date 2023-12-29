using Distance.PlacesClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPlacesClientModule(builder.Configuration);

var app = builder.Build();

app.MapGet("/{iata}", async (string iata, IPlacesRestApi placesApi) =>
{
    var response = await placesApi.GetPlaceAsync(iata, default);
    return response.ResponseMessage.IsSuccessStatusCode
        ? $"Hello from Distance & Places: {response.StringContent}!"
        : $"Hello from Distance: {iata}!";
});


app.Run();
