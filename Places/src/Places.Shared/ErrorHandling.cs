using CSharpFunctionalExtensions;

namespace Places.Shared;

public static class ErrorHandling
{
    public const string ErrorSeparator = " || ";

    public static Result Combine(params Result[] results) =>
        Result.Combine(ErrorSeparator, results);

    public static Result<T> FailWith<T>(string error) =>
        Result.Failure<T>(error);
}