using MediatR;

namespace WageTheftAnalyzer.Features.Inflation;
public partial class Inflation
{
    public partial class FetchInflation
    {
        public class Query : IRequest<Response>
        {
            public Query(DateTime date, string country)
            {
                Date = date;
                Country = country;
            }
            public DateTime Date { get; }
            public string Country { get; }
        }

        public class Response
        {
            public Response(DateTime date, decimal inflationPercentage, string country)
            {
                Date = date;
                InflationPercentage = inflationPercentage;
                Country = country;
            }
            public decimal InflationPercentage { get; }
            public DateTime Date { get; }
            public string Country { get; }
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
