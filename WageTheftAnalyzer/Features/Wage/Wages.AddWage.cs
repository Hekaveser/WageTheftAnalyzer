using MediatR;
using static WageTheftAnalyzer.Features.Wage.Wages.AddWage;

namespace WageTheftAnalyzer.Features.Wage;

public partial class Wages
{
    public partial class AddWage
    {
        public class Command : IRequest
        {
            public Command(decimal wage, Currency currency, DateTime date, int userId)
            {
                Wage = wage;
                Currency = currency;
                Date = date;
                UserId = userId;
            }
            public decimal Wage { get; }
            public Currency Currency { get; }
            public DateTime Date { get; }
            public int UserId { get; }
        }

        public class Handler : IRequestHandler<Command>
        {
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }

    //public static IServiceCollection AddAddWage(this IServiceCollection services)
    //{
    //    //todo - všechno řeší mediatR, leda by něco přibylo mimo něj
    //}
    public class WageDto
    {
        public decimal Wage { get; }
        public Currency Currency { get; }
        public int UserId { get; }
    }

    public static IEndpointRouteBuilder MapAddWage(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/wages", async (IMediator mediator, WageDto wageDto, CancellationToken cancellationToken) =>
        {
            Command command = new(wageDto.Wage, wageDto.Currency, DateTime.Now, wageDto.UserId);
            await mediator.Send(command, cancellationToken);
        })
            .WithName(nameof(AddWage))
            .WithSummary("Uložení mzdy v čase.")
            .Produces(StatusCodes.Status201Created);
        return endpoints;
    }
}
