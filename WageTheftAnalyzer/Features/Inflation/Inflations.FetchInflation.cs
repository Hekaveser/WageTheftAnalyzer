using MediatR;

namespace WageTheftAnalyzer.Features.Inflation;
public partial class Inflation
{
    public partial class FetchInflation
    {
        public class Query : IRequest<Response>
        {
            public Query(DateTime date)
            {
                Date = date;
            }
            public DateTime Date { get; }
        }

        public class Response
        {
            public Response(DateTime date, decimal inflationPercentage)
            {
                Date = date;
                InflationPercentage = inflationPercentage;
            }
            public decimal InflationPercentage { get; }
            public DateTime Date { get; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
