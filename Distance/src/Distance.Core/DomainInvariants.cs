using System.Text.RegularExpressions;
using Distance.Core.Domain;

namespace Distance.Core;

public sealed partial class DomainInvariants
{
    [GeneratedRegex(Iata.CodePattern)]
    public static partial Regex IataPattern();
}