using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using NSubstitute;
using Places.Api.Models.Responses;
using Places.Core.Contracts.Elastic;
using Places.Core.Contracts.Models;
using Places.Core.Domain;

namespace Places.Api.Tests;

public class PlacesServiceTests
{
    private readonly AirportDto ams;

    public PlacesServiceTests()
    {
        ams = new AirportDto
        {
            Id = 1,
            Name = "Amsterdam Airport Schiphol",
            IcaoCode = "EHAM",
            IataCode = "AMS",
            Type = "large_airport",
            Continent = "EU",
            ScheduledService = "yes",
            Location = new LocationDto
            {
                Latitude = 52.3086, Longitude = 4.7639
            }
        };
    }

    [Fact(DisplayName = "Should successfully return valid response")]
    public async Task Test001()
    {
        var airport = Airport.Parse(ams);
        airport.Should().Succeed();

        var repository = Substitute.For<IAirportsRepository>();
        repository.GetByIataAsync(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(airport.Value);

        await using var factory = new PlacesAppFactory(services =>
        {
            services.RemoveAll<IHostedService>();
            services.Replace(ServiceDescriptor.Scoped(_ => repository));
        });

        var client = factory.CreateClient();
        var responseMessage = await client.GetAsync("/api/airports/AMS");
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseString = await responseMessage.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<AirportResponse>(responseString);
        result.Should().NotBeNull();

        result!.Id.Should().Be(ams.Id);
        result.Name.Should().Be(ams.Name);
        result.IcaoCode.Should().Be(ams.IcaoCode);
        result.IataCode.Should().Be(ams.IataCode);
        result.Location.Should().Be(ams.Location);
    }
}