using CSharpFunctionalExtensions;
using Places.Core.Contracts.Elastic;
using Places.Core.Contracts.Models;
using Places.Core.Domain;
using static Places.Infra.Elastic.IndexNames;

namespace Places.Infra.Elastic;

public class AirportsRepository : IAirportsRepository
{
    private readonly IElasticClientFactory factory;

    public AirportsRepository(IElasticClientFactory factory)
    {
        this.factory = factory;
    }

    public async Task<Maybe<Airport>> GetByIataAsync(string iataCode, CancellationToken token = default)
    {
        var searchResponse = await factory.GetClient()
            .SearchAsync<AirportDto>(index =>
                index.Indices(AirportsIndexName)
                    .Query(query =>
                        query.Term(termQuery =>
                            termQuery.Field("iataCode.keyword").Value(iataCode)
                        )
                    ), token);

        if (!searchResponse.IsValidResponse || searchResponse.Total == 0)
            return Maybe<Airport>.None;

        /* NOTE:
           Log parse error, there is an incorrect data in DB.
           User have nothing to do with it, so for user it's '404'. */
        var airport = Airport.Parse(searchResponse.Documents.First());

        return airport.IsFailure
            ? Maybe<Airport>.None
            : airport.Value;
    }
}