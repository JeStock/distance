using Distance.Application;
using Distance.Core.Contracts.Services;

namespace Distance.Api.Composition;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services) =>
        services.AddScoped<IDistanceService, DistanceService>();
}