namespace Places.Core.Domain.Enums;

public enum ScheduledService
{
    Yes = 1,
    No = 2
}

public static class ScheduledServiceExtensions
{
    public static ScheduledService? MapToDomain(this Contracts.Models.ScheduledService? service) =>
        service switch
        {
            Contracts.Models.ScheduledService.Yes => ScheduledService.Yes,
            Contracts.Models.ScheduledService.No => ScheduledService.No,
            _ => (ScheduledService?)default
        };

    public static Contracts.Models.ScheduledService ToDto(this ScheduledService type) =>
        type switch
        {
            ScheduledService.Yes => Contracts.Models.ScheduledService.Yes,
            ScheduledService.No => Contracts.Models.ScheduledService.Yes
        };
}