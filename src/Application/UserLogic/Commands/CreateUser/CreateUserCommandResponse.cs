using Domain.UserAggregate;

namespace Application.UserLogic.Commands.CreateUser
{
    public record CreateUserCommandResponse(UserId Id, string Token);
}
