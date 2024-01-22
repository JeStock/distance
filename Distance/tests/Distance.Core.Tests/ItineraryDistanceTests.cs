using Distance.Core.Contracts.Models;
using Distance.Core.Domain;
using FluentAssertions;

namespace Distance.Core.Tests;

public class ItineraryDistanceTests
{
    [Fact(DisplayName = "Should successfully parse airport data and calculate distance between them")]
    public void Test003()
    {
        var ams = new AirportDto
        {
            Id = 2513,
            Name = "Amsterdam Airport Schiphol",
            IcaoCode = "EHAM",
            IataCode = "AMS",
            Location = new LocationDto
            {
                Latitude = 52.308601, Longitude = 4.76389
            }
        };

        var cdg = new AirportDto
        {
            Id = 4185,
            Name = "Charles de Gaulle International Airport",
            IcaoCode = "LFPG",
            IataCode = "CDG",
            Location = new LocationDto
            {
                Latitude = 49.012798, Longitude = 2.55
            }
        };

        var distanceResult = ItineraryDistance.Parse(ams, cdg);
        distanceResult.Should().Succeed();

        var distance = distanceResult.Value;

        Math.Round(distance.DistanceValue, 1).Should().Be(398.3);
        distance.Units.Should().Be("km");
        distance.OriginAirportName.Should().Be(ams.Name);
        distance.DestinationAirportName.Should().Be(cdg.Name);
    }
}