using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WageTheftAnalyzer.Features.Inflation;

public partial class Inflations
{
    public class Query : IRequest<Response>
    {
        public Query(DateCountryPair[] dateCountryPairs)
        {
            DateCountryPairs = dateCountryPairs;
        }
        public IEnumerable<DateCountryPair> DateCountryPairs { get; }
    }

    public record DateCountryPair(DateTime Date, string Country);

    public class Response
    {
        public Response(IEnumerable<InflationDto> inflations)
        {
            Inflations = inflations;
        }
        public IEnumerable<InflationDto> Inflations { get; }
    }

    public class InflationDto
    {
        public InflationDto(int id, DateTime date, string country, decimal percentageRate)
        {
            Id = id;
            Date = date;
            Country = country;
            PercentageRate = percentageRate;
        }
        public int Id { get; }
        public DateTime Date { get; }
        public string Country { get; }
        public decimal PercentageRate { get; }
    }

    public class Handler : IRequestHandler<Query, Response>
    {
        public Handler(InflationContext inflationContext)
        {
            this.inflationContext = inflationContext;
        }

        private readonly InflationContext inflationContext;

        public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
        {
            InflationDto[] inflations = await (from inflation in inflationContext.Inflations
                                               from pair in request.DateCountryPairs
                                               where
                                               pair.Country == inflation.Country &&
                                               pair.Date.Year == inflation.Date.Year &&
                                               pair.Date.Month == inflation.Date.Month
                                               select new InflationDto(inflation.Id, inflation.Date, inflation.Country, inflation.Percentage))
                                               .ToArrayAsync(cancellationToken);
            return new Response(inflations);
        }
    }
}
