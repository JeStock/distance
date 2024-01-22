using AutoFixture;
using FluentAssertions;
using FsCheck;
using Places.Core.Contracts.Models;
using Places.Core.Domain;
using Places.Core.Domain.Enums;
using Places.Shared;
using Xunit.Abstractions;
using static FsCheck.Xunit.CheckExtensions;

namespace Places.Core.Tests;

public class AirportTests(ITestOutputHelper testOutputHelper)
{
    private readonly IFixture fixture = new Fixture();

    private static readonly string[] IataCodes = ["AMS", "BCN", "MAD", "CDG", "FRA", "BER"];
    private static readonly string[] IcaoCodes = ["EHAM", "LEBL", "LEMD", "LFPG", "EDDF", "EDDB"];

    private static Gen<T> ChooseFrom<T>(IReadOnlyList<T> xs) => Gen.Choose(0, xs.Count - 1).Select(i => xs[i]);

    private static Arbitrary<AirportDto> GenAirportData(IFixture fixture) =>
        Arb.From(
            from service in Arb.From<ScheduledService>().Generator
            from type in Arb.From<AirportType>().Generator    // Property: There is an exhaustive list of possible airport type
            from continent in Arb.From<Continent>().Generator // Property: There is an exhaustive list of possible Continents
            from iata in ChooseFrom(IataCodes)                // Property: IATA has to be 3 upper case letters
            from icao in ChooseFrom(IcaoCodes)                // Property: ICAO has to be 4 upper case letters
            from lat in Gen.Choose(-90, 90)                   // Property: latitude has an invariant - [-90;90]
            from lon in Gen.Choose(-180, 180)                 // Property: longitude has an invariant - [-90;90]
            select fixture.Build<AirportDto>()
                .With(x => x.Type, AirportTypeParser.ToString(type))
                .With(x => x.ScheduledService, ScheduledServiceParser.ToString(service))
                .With(x => x.Continent, ContinentParser.ToString(continent))
                .With(x => x.IataCode, iata)
                .With(x => x.IcaoCode, icao)
                .With(x => x.Location, new LocationDto
                {
                    Latitude = lat, Longitude = lon
                }).Create()
        ).Generator.ToArbitrary();

    [Fact(DisplayName = "Should successfully parse valid Airport data")]
    public void Test001() =>
        Prop.ForAll(GenAirportData(fixture), dto => Airport.Parse(dto).IsSuccess)
            .VerboseCheck(testOutputHelper);

    [Fact(DisplayName = "Should collect all errors on parsing invalid Airport data")]
    public void Test002()
    {
        var dto = new AirportDto
        {
            Id = 1,
            Name = null,
            IcaoCode = "INVALID_ICAO_CODE",
            IataCode = "INVALID_IATA_CODE",
            Type = "INVALID_TYPE",
            Continent = "INVALID_CONTINENT",
            ScheduledService = "INVALID_SCHEDULED_SERVICE",
            Location = new LocationDto
            {
                Latitude = 1000, Longitude = 2000
            }
        };

        var result = Airport.Parse(dto);
        result.IsFailure.Should().BeTrue();
        result.Error.Split(ErrorHandling.ErrorSeparator).Should().BeEquivalentTo([
            "Name is invalid",
            $"'{dto.IcaoCode}' doesn't match ICAO pattern '^[A-Z]{{4}}$'",
            $"'{dto.IataCode}' doesn't match IATA pattern '^[A-Z]{{3}}$'",
            "Airport type is invalid",
            "Continent code is invalid",
            "ScheduledService is invalid",
            $"'{dto.Location.Latitude}' is invalid Latitude",
            $"'{dto.Location.Longitude}' is invalid Longitude"
        ]);
    }

    [Fact(DisplayName = "Should successfully parse valid Airport data and map it to dto")]
    public void Test003()
    {
        var dto = new AirportDto
        {
            Id = 2513,
            Name = "Amsterdam Airport Schiphol",
            IcaoCode = "EHAM",
            IataCode = "AMS",
            Type = "large_airport",
            ScheduledService = "yes",
            Continent = "EU",
            Location = new LocationDto
            {
                Latitude = 52.3086, Longitude = 4.7639
            }
        };

        var result = Airport.Parse(dto);
        result.Should().Succeed();
        result.Value.ToDto().Should().BeEquivalentTo(dto);
    }
}