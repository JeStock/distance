using Places.Api.Configuration.Settings;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using ILogger = Serilog.ILogger;

namespace Places.Api.Configuration;

public static class LoggingConfiguration
{
    public static ILogger InitSerilog() =>
        new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Async(x => x.Console())
            .CreateBootstrapLogger();

    public static IHostBuilder ConfigureSerilog(this IHostBuilder builder) =>
        builder.UseSerilog((context, services, config) => config
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext()
            .WriteTo.Async(x => x.Console())
            .WriteTo.Async(x =>
            {
                var elasticConf = context.Configuration.GetSection<ElasticsearchOptions>();
                if (elasticConf != null)
                    x.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticConf.LogUrl)));
            }));
}