using CSharpFunctionalExtensions;
using static Places.Core.ErrorHandling;

namespace Places.Core.Domain;

public record Icao
{
    public const string CodePattern = "^[A-Z]{4}$";

    public string Code { get; private set; }

    private Icao(string code) => Code = code;

    public static Result<Icao> Parse(string? code) =>
        code == null || DomainInvariants.IcaoPattern().IsMatch(code) == false
            ? FailWith<Icao>($"Icao code doesn't match pattern '{CodePattern}'")
            : new Icao(code);
}