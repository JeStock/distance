using CSharpFunctionalExtensions;
using static Places.Core.Domain.Enums.ScheduledService;
using static Places.Shared.ErrorHandling;

namespace Places.Core.Domain.Enums;

public enum ScheduledService
{
    Yes = 1,
    No = 2
}

public static class ScheduledServiceParser
{
    public static Result<ScheduledService> Parse(string? serviceStr) =>
        serviceStr switch
        {
            "yes" => Yes,
            "no" => No,
            _ => FailWith<ScheduledService>("ScheduledService is invalid")
        };

    public static string ToString(this ScheduledService service) =>
        service switch
        {
            Yes => "yes",
            No => "no",
        };
}