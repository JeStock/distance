using Distance.Api.Composition;
using Distance.Api.Configuration;
using Serilog;

Log.Logger = LoggingConfiguration.InitSerilog();

try
{
    Log.Logger.Information("Bootstrapping Distance service");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddSwaggerGen()
        .AddControllers(options => options.AddRoutesConventions())
        .AddControllersAsServices();

    builder.Services
        .AddInfrastructureModule(builder.Configuration)
        .AddApplicationService();

    builder.Host.ConfigureSerilog();
    var app = builder.Build();

    if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Local"))
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}