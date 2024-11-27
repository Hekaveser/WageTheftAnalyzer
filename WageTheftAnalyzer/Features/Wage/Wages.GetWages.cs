using MediatR;
using Microsoft.EntityFrameworkCore;
using WageTheftAnalyzer.Features.Inflation;
using static WageTheftAnalyzer.Features.Inflation.Inflations;

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
            public Handler(WageContext wageContext, IMediator mediator)
            {
                this.wageContext = wageContext;
                this.mediator = mediator;
            }

            private readonly WageContext wageContext;
            private readonly IMediator mediator;

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                //todo - add fluent validation and validate inputs here
                int userId = request.UserId;
                Wage[] wageInflations = await (from wages in wageContext.Wages
                                               where wages.UserId == userId &&
                                               (wages.Date >= request.From && wages.Date <= request.To)
                                               select wages).ToArrayAsync(cancellationToken);

                Inflations.Query query = new(wageInflations
                    .Select(w => new DateCountryPair(w.Date, w.Country))
                    .ToArray());
                Inflations.Response inflations = await mediator.Send(query, cancellationToken);

                List<WageInflationDto> wageInflationDtoList = MergeInflationsWithWages(wageInflations, inflations);

                WagesInflationsDto wagesInflationsDto = new(userId, [.. wageInflationDtoList]);
                return new Response(wagesInflationsDto);
            }

            private static List<WageInflationDto> MergeInflationsWithWages(Wage[] wageInflations, Inflations.Response inflations)
            {
                List<WageInflationDto> wageInflationDtoList = [];
                foreach (Wage wageInflation in wageInflations)
                {
                    InflationDto? inflation = inflations.Inflations
                        .Where(i => i.Country == wageInflation.Country
                        && (i.Date.Month == wageInflation.Date.Month && i.Date.Year == wageInflation.Date.Year))
                        .FirstOrDefault();

                    wageInflationDtoList.Add(new WageInflationDto(wageInflation.Amount,
                        wageInflation.Date,
                        wageInflation.Currency,
                        inflation?.PercentageRate ?? 0));
                }
                return wageInflationDtoList;
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
