﻿using CSharpFunctionalExtensions;
using static Places.Shared.ErrorHandling;

namespace Places.Core.Domain;

public record Iata
{
    public const string CodePattern = "^[A-Z]{3}$";

    public string Code { get; private set; }

    private Iata(string code) => Code = code;

    public static Result<Iata> Parse(string? code) =>
        code == null || DomainInvariants.IataPattern().IsMatch(code) == false
            ? FailWith<Iata>($"'{code}' doesn't match IATA pattern '{CodePattern}'")
            : new Iata(code);
}