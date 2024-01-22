namespace Places.Core.Contracts.Models;

public record LocationDto
{
    public double? Latitude { get; init; }
    public double? Longitude { get; init; }
}