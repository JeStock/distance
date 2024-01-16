using System.Diagnostics.CodeAnalysis;

namespace Distance.Core.Domain;

[method: SetsRequiredMembers]
public class DistanceDto(double distance, string units)
{
    public required double Distance { get; init; } = distance;
    public required string Units { get; init; } = units;

    [SetsRequiredMembers]
    public DistanceDto(double distance) : this(distance, "km") { }
}