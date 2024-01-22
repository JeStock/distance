using CSharpFunctionalExtensions;
using Places.Core.Domain;

namespace Places.Core.Contracts.Services;

public interface IAirportsService
{
    Task<Result<Airport>> GetByIataAsync(Iata iata, CancellationToken token = default);
}