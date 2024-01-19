using CSharpFunctionalExtensions;

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

    public static Result<Itinerary> Parse(string origin, string destination)
    {
        var originIata = Iata.Parse(origin);
        var destinationIata = Iata.Parse(destination);

        return Result.Combine(originIata, destinationIata)
            .Map(() => new Itinerary(originIata.Value, destinationIata.Value));
    }
}