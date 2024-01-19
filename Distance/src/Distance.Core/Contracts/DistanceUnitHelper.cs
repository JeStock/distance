using Geolocation;

namespace Distance.Core.Contracts;

public static class DistanceUnitHelper
{
    public static string ToString(this DistanceUnit type) =>
        type switch
        {
            DistanceUnit.Miles => "mi",
            DistanceUnit.Kilometers => "km",
            DistanceUnit.NauticalMiles => "nm",
            DistanceUnit.Meters => "m"
        };
}