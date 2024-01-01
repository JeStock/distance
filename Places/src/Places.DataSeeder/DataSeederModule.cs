using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Places.DataSeeder;

public static class DataSeederModule
{
    public static IServiceCollection AddDataSeederModule(this IServiceCollection services, IConfiguration config)
    {
        return services.AddHostedService<DataSeedBackgroundWorker>();
    }
}