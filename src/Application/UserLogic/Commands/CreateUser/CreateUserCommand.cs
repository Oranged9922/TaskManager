using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Application.UserLogic.Commands.CreateUser
{
    public record CreateUserCommand(string Username, string Email, string Password) : IRequest<ErrorOr<CreateUserCommandResponse>>, IAllowAnonymous;
}
