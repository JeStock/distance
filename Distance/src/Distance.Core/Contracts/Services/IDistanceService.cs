using CSharpFunctionalExtensions;
using Distance.Core.Domain;

namespace Distance.Core.Contracts.Services;

public interface IDistanceService
{
    Task<Result<ItineraryDistance>> GetDistanceAsync(Itinerary itinerary, CancellationToken token = default);
}