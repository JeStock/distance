using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Places.Infra.Csv;

namespace Places.DataSeeder;

public static class DataSeederModule
{
    public static IServiceCollection AddDataSeederModule(this IServiceCollection services, IConfiguration config)
        => services
            .AddScoped<IAirportsHandler, AirportsHandler>()
            .AddHostedService<DataSeedBackgroundWorker>();
}