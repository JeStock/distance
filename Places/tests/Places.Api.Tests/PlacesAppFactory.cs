using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Places.Api.Tests;

public class PlacesAppFactory : WebApplicationFactory<Program>
{
    private readonly Action<IServiceCollection>? dependencies;

    public PlacesAppFactory(Action<IServiceCollection>? dependencies = null) =>
        this.dependencies = dependencies;

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.ConfigureServices(services => dependencies?.Invoke(services));
}