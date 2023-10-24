using Domain.UserAggregate;

namespace Contracts.User.CreateUser
{
    public record CreateUserResponse(string UserId, string JwtToken);
}
