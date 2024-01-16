using System.ComponentModel.DataAnnotations;

namespace Distance.Api.Models;

public class DistanceQuery
{
    [Required, RegularExpression("^[A-Z]{3}$")]
    public required string Origin { get; init; }

    [Required, RegularExpression("^[A-Z]{3}$")]
    public required string Destination { get; init; }
}