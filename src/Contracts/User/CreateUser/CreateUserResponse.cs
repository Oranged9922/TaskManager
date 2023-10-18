using Domain.UserAggregate;

namespace Contracts.User.CreateUser
{
    public record CreateUserResponse(UserId UserId, string JwtToken);
}
