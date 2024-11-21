using MediatR;
using static WageTheftAnalyzer.Features.User.Users.RegisterUser;

namespace WageTheftAnalyzer.Features.User;

public partial class Users
{
    public partial class RegisterUser
    {
        public class Command : IRequest
        {
            public Command(string userName, string email)
            {
                UserName = userName;
                Email = email;
            }
            public string UserName { get; }
            public string Email { get; }
        }

        public class Handler : IRequestHandler<Command>
        {
            public Task Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }

    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public static IEndpointRouteBuilder MapRegisterUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/users", async (IMediator mediator, RegisterUserDto user, CancellationToken cancellationToken) =>
        {
            Command command = new(user.UserName, user.Email);
            await mediator.Send(command, cancellationToken);
        })
            .WithName(nameof(RegisterUser))
            .WithSummary("Registrace nového uživatele.")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        return endpoints;
    }
}
