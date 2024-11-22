using MediatR;
using Microsoft.EntityFrameworkCore;

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
            public Response(WagesInflationsDto wagesWithInflation)
            {
                WagesWithInflation = wagesWithInflation;
            }
            public WagesInflationsDto? WagesWithInflation { get; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            public Handler(IDbContextFactory<WageContext> dbContextFactory, IMediator mediator)
            {
                this.dbContextFactory = dbContextFactory;
                this.mediator = mediator;
            }

            private readonly IDbContextFactory<WageContext> dbContextFactory;
            private readonly IMediator mediator;

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                Wage[] wageInflations = [];
                int userId = request.UserId;
                using (WageContext wageContext = dbContextFactory.CreateDbContext())
                {
                    wageInflations = await (from wages in wageContext.Wages
                                            where wages.UserId == userId &&
                                            (wages.Date >= request.From && wages.Date <= request.To)
                                            select wages).ToArrayAsync(cancellationToken);
                }
                //todo - slice for load inflation records from database in Inflation feature

                throw new NotImplementedException();

                //WagesInflationsDto wagesInflationsDto = new(userId, wageInflations);
                //return new Response(wagesInflationsDto);
            }
        }
    }

    public class WagesInflationsDto
    {
        public WagesInflationsDto(int userId, WageInflationDto[] wageInflations)
        {
            UserId = userId;
            WageInflations = wageInflations ?? [];
        }
        public int UserId { get; }
        public WageInflationDto[] WageInflations { get; }
    }

    public class WageInflationDto
    {
        public WageInflationDto(decimal wage, DateTime date, Currency currency, decimal inflationPercentageRate)
        {
            Wage = wage;
            Date = date;
            Currency = currency;
            InflationPercentageRate = inflationPercentageRate;
        }
        public decimal Wage { get; }
        public DateTime Date { get; }
        public Currency Currency { get; }
        public decimal InflationPercentageRate { get; }
    }

    public static IEndpointRouteBuilder MapGetWages(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/wages", async (DateTime dateFrom, DateTime dateTo, IMediator mediator, CancellationToken cancellationToken) =>
        {
            int userId = 0; //todo - get UserId from header and token
            GetWages.Query query = new(userId, dateFrom, dateTo);
            GetWages.Response response = await mediator.Send(query, cancellationToken);
            return response;
        });
        return endpoints;
    }
}
