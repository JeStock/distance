using CSharpFunctionalExtensions;
using Places.Core.Domain;

namespace Places.Core.Contracts.Elastic;

public interface IAirportsRepository
{
    Task<Maybe<Airport>> GetByIataAsync(string iataCode, CancellationToken token = default);
}