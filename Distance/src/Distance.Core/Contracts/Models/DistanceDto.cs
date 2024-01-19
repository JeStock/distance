using System.Diagnostics.CodeAnalysis;
using Geolocation;

namespace Distance.Core.Contracts.Models;

public class DistanceDto
{
    public required double Distance { get; init; }
    public required string Units { get; init; }

    public DistanceDto() { }

    [SetsRequiredMembers]
    public DistanceDto(double distance)
    {
        Distance = distance;
        Units = DistanceUnit.Kilometers.ToString();
    }
}