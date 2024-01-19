using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;
using Distance.Core.Domain;

namespace Distance.Core.Contracts.Services;

public interface IDistanceService
{
    Task<Result<DistanceDto>> GetDistanceAsync(Itinerary itinerary, CancellationToken token = default);
}