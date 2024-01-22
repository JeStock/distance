using System.Net;
using CSharpFunctionalExtensions;
using Distance.Api.Models.Responses;
using Distance.Core.Contracts.Models;
using Distance.Core.Contracts.Providers;
using Distance.Core.Domain;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using NSubstitute;

namespace Distance.Api.Tests;

public class DistanceServiceTests
{
    private readonly AirportDto ams;
    private readonly AirportDto bcn;
    private readonly IAirportsProvider airportsProvider;

    public DistanceServiceTests()
    {
        ams = new()
        {
            Id = 1,
            Name = "Amsterdam Airport Schiphol",
            IcaoCode = "EHAM",
            IataCode = "AMS",
            Location = new LocationDto
            {
                Latitude = 52.3105, Longitude = 4.7683
            }
        };

        bcn = new()
        {
            Id = 2,
            Name = "Barcelona Airport",
            IcaoCode = "LEBL",
            IataCode = "BCN",
            Location = new LocationDto
            {
                Latitude = 41.2974, Longitude = 2.0833
            }
        };

        airportsProvider = Substitute.For<IAirportsProvider>();
        airportsProvider.GetItineraryAirportsAsync(Arg.Any<Itinerary>(), Arg.Any<CancellationToken>())
            .Returns(Result.Success((ams, bcn)));
    }

    [Fact(DisplayName = "Should successfully return valid response")]
    public async Task Test001()
    {
        var distance = ItineraryDistance.Parse(ams, bcn);
        distance.Should().Succeed();

        await using var factory = new DistanceAppFactory(services =>
            services.Replace(ServiceDescriptor.Scoped(_ => airportsProvider)));

        var client = factory.CreateClient();
        var responseMessage = await client.GetAsync("/api/distance?Origin=AMS&Destination=BCN");
        responseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseString = await responseMessage.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<DistanceResponse>(responseString);
        result.Should().NotBeNull();

        result!.Distance.Should().Be(distance.Value.DistanceValue);
    }
}