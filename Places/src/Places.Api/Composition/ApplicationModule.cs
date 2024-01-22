using Places.Application;
using Places.Core.Contracts.Services;

namespace Places.Api.Composition;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services) =>
        services.AddScoped<IAirportsService, AirportsService>();
}