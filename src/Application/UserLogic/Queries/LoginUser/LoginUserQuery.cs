using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Application.UserLogic.Queries.LoginUser
{
    public record LoginUserQuery(string Username, string Password) : IRequest<ErrorOr<LoginUserQueryResponse>>, IAllowAnonymous;
}
