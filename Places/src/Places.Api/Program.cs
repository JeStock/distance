var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{iata}", (string iata) => $"Places responded '{iata}'");

app.Run();
