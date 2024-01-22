using System.Text.RegularExpressions;
using Places.Core.Domain;

namespace Places.Core;

public sealed partial class DomainInvariants
{
    [GeneratedRegex(Iata.CodePattern)]
    public static partial Regex IataPattern();

    [GeneratedRegex(Icao.CodePattern)]
    public static partial Regex IcaoPattern();
}