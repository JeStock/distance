using CSharpFunctionalExtensions;
using static Distance.Shared.ErrorHandling;

namespace Distance.Core.Domain;

public class Iata
{
    public const string CodePattern = "^[A-Z]{3}$";

    public string Code { get; private set; }

    private Iata(string code) => Code = code;

    public static Result<Iata> Parse(string? code) =>
        code == null || DomainInvariants.IataPattern().IsMatch(code) == false
            ? FailWith<Iata>($"Iata code doesn't match pattern '{CodePattern}'")
            : new Iata(code);
}