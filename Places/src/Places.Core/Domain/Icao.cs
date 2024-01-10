namespace Places.Core.Domain;

public record Icao
{
    public const string CodePattern = "^[A-Z]{4}$";

    public string Code { get; private set; }

    private Icao(string code) => Code = code;

    public static Icao? Create(string? code)
    {
        if (code == null || DomainInvariants.IcaoPattern().IsMatch(code) == false)
            return default;

        return new Icao(code);
    }
}