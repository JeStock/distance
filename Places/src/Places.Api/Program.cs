using Places.Api.Configuration;
using Places.DataSeeder;
using Places.Infra;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Async(x => x.Console())
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, config) => config
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Async(x => x.Console())
        // TODO [sg]: add Elasticsearch + structured logs
    );

    builder.Services
        .AddSwaggerGen()
        .AddControllers(options => options.AddRoutesConventions())
        .AddControllersAsServices();

    builder.Services
        .AddInfraModule(builder.Configuration)
        .AddDataSeederModule(builder.Configuration);

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