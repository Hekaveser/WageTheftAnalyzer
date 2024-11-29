using MediatR;
using Microsoft.EntityFrameworkCore;

namespace WageTheftAnalyzer.Features.Inflation;

public partial class Inflations
{
    public record Query(DateTime From, DateTime To, string Country) : IRequest<Response>;

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
        public InflationDto(int id, DateTime from, DateTime to, string country, decimal percentageRate)
        {
            Id = id;
            From = from;
            To = to;
            Country = country;
            PercentageRate = percentageRate;
        }
        public int Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
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

        public async Task<Response> Handle(Query query, CancellationToken cancellationToken)
        {
            InflationDto[] inflations = await (from inflation in inflationContext.Inflations
                                               where inflation.From >= query.From && inflation.To <= query.To
                                               select new InflationDto(inflation.Id, inflation.From, inflation.To, inflation.Country, inflation.Rate))
                                               .ToArrayAsync(cancellationToken);
            return new Response(inflations);
        }
    }
}
