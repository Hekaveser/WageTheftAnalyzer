using MediatR;
using Microsoft.EntityFrameworkCore;
using WageTheftAnalyzer.Features.Inflation;
using static WageTheftAnalyzer.Features.Inflation.Inflations;

namespace WageTheftAnalyzer.Features.Wage;

public partial class Wages
{
    public partial class GetWages
    {
        public record Query(DateTime From, DateTime To, int UserId, string Country) : IRequest<Response>;
        public record Response(WagesInflationsDto WagesInflationsDto);

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
                                               wages.Country == request.Country &&
                                               (wages.From >= request.From && wages.To <= request.To)
                                               select wages).ToArrayAsync(cancellationToken);

                Inflations.Query query = new(request.From, request.To, request.Country);
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
                    IEnumerable<InflationDto>? relevantInflation = inflations.Inflations
                        .Where(i => i.Country == wageInflation.Country
                        && (wageInflation.From <= i.From && wageInflation.To >= i.To));

                    decimal averageInflation = relevantInflation?.Average(inf => inf.PercentageRate) ?? 0;

                    wageInflationDtoList.Add(new WageInflationDto(wageInflation.Amount,
                        wageInflation.From,
                        wageInflation.To,
                        wageInflation.Currency,
                        averageInflation));
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
        public WageInflationDto(decimal wage, DateTime from, DateTime to, Currency currency, decimal inflationPercentageRate)
        {
            Wage = wage;
            From = from;
            To = to;
            Currency = currency;
            InflationPercentageRate = inflationPercentageRate;
        }
        public decimal Wage { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public Currency Currency { get; }
        public decimal InflationPercentageRate { get; }
    }

    public static IEndpointRouteBuilder MapGetWages(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/wages", async (DateTime from, DateTime to, IMediator mediator, CancellationToken cancellationToken) =>
        {
            int userId = 0; //todo - get UserId from header and token.
            string country = string.Empty; //todo - uživatel toto bude mít v nastavení.
            GetWages.Query query = new(from, to, userId, country);
            GetWages.Response response = await mediator.Send(query, cancellationToken);
            return response;
        });
        return endpoints;
    }
}
