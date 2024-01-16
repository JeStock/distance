using CSharpFunctionalExtensions;
using Distance.Core.Contracts.Models;

namespace Distance.Core.Contracts.Providers;

public interface IAirportsProvider
{
    Task<Maybe<AirportDto>> GetAsync(string iata, CancellationToken token);
}