using CSharpFunctionalExtensions;
using Distance.Core.Domain;

namespace Distance.Core.Contracts.Services;

public interface IDistanceService
{
    Task<Result<Domain.Distance>> GetDistanceAsync(Itinerary itinerary, CancellationToken token = default);
}