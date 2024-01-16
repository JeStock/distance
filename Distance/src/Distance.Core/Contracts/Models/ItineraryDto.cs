using System.Diagnostics.CodeAnalysis;

namespace Distance.Core.Contracts.Models;

[method: SetsRequiredMembers]
public class ItineraryDto(string origin, string destination)
{
    public required string Origin { get; init; } = origin;
    public required string Destination { get; init; } = destination;
}