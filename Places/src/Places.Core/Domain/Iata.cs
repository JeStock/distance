using static Places.Core.DomainInvariants;

namespace Places.Core.Domain;

public record Iata
{
    public const string CodePattern = "^[A-Z]{3}$";

    public string Code { get; private set; }

    private Iata(string code) => Code = code;

    public static Iata? Create(string? code)
    {
        if (code == null || IataPattern().IsMatch(code) == false)
            return default;

        return new Iata(code);
    }
}