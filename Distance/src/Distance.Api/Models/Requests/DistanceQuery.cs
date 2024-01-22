using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace Distance.Api.Models.Requests;

public class DistanceQuery
{
    [Required, RegularExpression("^[A-Z]{3}$")]
    [SwaggerParameter("Departure Airport's IATA (3 upper case letters). E.g. AMS, BCN, MAD, CDG, FRA, BER")]
    public required string Origin { get; init; }

    [Required, RegularExpression("^[A-Z]{3}$")]
    [SwaggerParameter("Arrival Airport's IATA (3 upper case letters). E.g. AMS, BCN, MAD, CDG, FRA, BER")]
    public required string Destination { get; init; }
}