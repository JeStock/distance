using Microsoft.OpenApi.Models;
using Places.Api.Composition;
using Places.Api.Configuration;
using Serilog;

Log.Logger = LoggingConfiguration.InitSerilog();

try
{
    Log.Logger.Information("Bootstrapping Places service");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services
        .AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Places Service Api"
            });

            options.EnableAnnotations();
        })
        .AddControllers(options => options.AddRoutesConventions())
        .AddControllersAsServices();

    builder.Services
        .AddInfrastructureModule(builder.Configuration)
        .AddDataSeederModule()
        .AddApplicationModule();

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

public partial class Program { }