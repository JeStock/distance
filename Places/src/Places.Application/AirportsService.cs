using CSharpFunctionalExtensions;
using Places.Core.Contracts.Elastic;
using Places.Core.Contracts.Services;
using Places.Core.Domain;

namespace Places.Application;

public class AirportsService(IAirportsRepository repository) : IAirportsService
{
    public Task<Result<Airport>> GetByIataAsync(Iata iata, CancellationToken token = default) =>
        repository.GetByIataAsync(iata.Code, token).ToResult("Airport not found");
}