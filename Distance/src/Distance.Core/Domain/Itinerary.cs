using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;

namespace Distance.Core.Domain;

public class Itinerary
{
    public Iata Origin { get; }
    public Iata Destination { get; }

    private Itinerary(Iata origin, Iata destination)
    {
        Origin = origin;
        Destination = destination;
    }

    public static Result<Itinerary> Parse(ItineraryDto dto)
    {
        var origin = Iata.Parse(dto.Origin);
        var destination = Iata.Parse(dto.Destination);

        return Result.Combine(origin, destination)
            .Map(() => new Itinerary(origin.Value, destination.Value));
    }

    public ItineraryDto ToDto() => new(Origin.Code, Destination.Code);
}