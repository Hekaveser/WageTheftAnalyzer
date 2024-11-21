using MediatR;

namespace WageTheftAnalyzer.Features.Wage;

public partial class Wages
{
    public partial class GetWages
    {
        public class Query : IRequest<Response>
        {
            public Query(int userId, DateTime from, DateTime to)
            {
                UserId = userId;
                From = from;
                To = to;
            }

            public int UserId { get; }
            public DateTime From { get; }
            public DateTime To { get; }
        }

        public class Response
        {
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            public Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class WagesInflationsDto
    {
        public int UserId { get; }
        public WageInflationDto[]? WageInflation { get; set; }
    }

    public class WageInflationDto
    {
        public decimal Wage { get; }
        public Currency Currency { get; }
        public decimal PercentageRate { get; }
    }
}
