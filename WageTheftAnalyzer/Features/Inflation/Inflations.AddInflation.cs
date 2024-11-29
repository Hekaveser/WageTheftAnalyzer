using MediatR;

namespace WageTheftAnalyzer.Features.Inflation;

public partial class Inflations
{
    public partial class AddInflation
    {
        public record Command(DateTime From, DateTime To, string Country, decimal Rate) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly InflationContext inflationContext;
            public Handler(InflationContext inflationContext)
            {
                this.inflationContext = inflationContext;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                Inflation inflation = new()
                {
                    From = request.From,
                    To = request.To,
                    Country = request.Country,
                    Rate = request.Rate
                };

                await inflationContext.AddAsync(inflation, cancellationToken);
                await inflationContext.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
