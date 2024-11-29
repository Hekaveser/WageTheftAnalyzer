using MediatR;

namespace WageTheftAnalyzer.Features.Inflation;
public partial class Inflation
{
    public partial class FetchInflation
    {
        public record Query(DateTime From, DateTime To, string Country) : IRequest<Response>;
        public record Response(DateTime From, DateTime To, string Country, decimal Rate);

        public class Handler : IRequestHandler<Query, Response>
        {
            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                //todo - inflace se uloží do databáze, pokud v ní ještě není a bude tak sloužit jako cache.
                throw new NotImplementedException();
            }
        }
    }
}
