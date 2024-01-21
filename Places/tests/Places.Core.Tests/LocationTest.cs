using FluentAssertions;
using Places.Core.Contracts.Models;
using Places.Core.Domain;

namespace Places.Core.Tests;

public class LocationTest
{
    [Fact]
    public void Test001()
    {
        var airportDto = new AirportDto
        {
            Id = 1,
            Name = "Test",
            Type = "large_airport",
            Continent = "NA",
            IataCode = "ABC",
            IcaoCode = "ABCD",
            ScheduledService = "yes",
            Location = new LocationDto
            {
                Longitude = 30, Latitude = 30
            }
        };

        var res = Airport.Parse(airportDto);
        res.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Test002()
    {
        var airportDto = new AirportDto
        {
            Id = 1,
            Name = "Test",
            Type = "large_airport",
            Continent = "NA",
            IataCode = "AB",
            IcaoCode = "ABC",
            ScheduledService = "yes",
            Location = new LocationDto
            {
                Longitude = 30, Latitude = 30
            }
        };

        var res = Airport.Parse(airportDto);
        res.IsFailure.Should().BeTrue();
    }
}