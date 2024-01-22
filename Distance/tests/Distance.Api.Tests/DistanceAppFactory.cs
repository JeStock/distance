using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Distance.Api.Tests;

public class DistanceAppFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? dependencies;

    public DistanceAppFactory(Action<IServiceCollection>? dependencies = null) =>
        this.dependencies = dependencies;

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureServices(services => dependencies?.Invoke(services));
}