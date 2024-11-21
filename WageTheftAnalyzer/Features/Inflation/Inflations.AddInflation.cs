using MediatR;

namespace WageTheftAnalyzer.Features.Inflation;

public partial class Inflations
{
    public partial class AddInflation
    {
        public class Command : IRequest
        {
            public DateTime Date { get; set; }
            public decimal PercentageRate { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

    }
}
