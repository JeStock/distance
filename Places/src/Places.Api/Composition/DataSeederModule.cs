using Places.Core.Contracts.Csv;
using Places.DataSeeder;
using Places.Infra.Csv;

namespace Places.Api.Composition;

public static class DataSeederModule
{
    public static IServiceCollection AddDataSeederModule(this IServiceCollection services, IConfiguration config) =>
        services
            .AddSingleton<IAirportsRepository, AirportsRepository>()
            .AddHostedService<DataSeedBackgroundWorker>();
}