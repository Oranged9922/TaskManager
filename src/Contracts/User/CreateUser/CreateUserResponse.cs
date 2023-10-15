using Domain.UserAggregate;

namespace Contracts.User.CreateUser
{
    public record class CreateUserResponse
    {
        public UserId UserId { get; init; } = default!;
        public string JwtToken { get; init; } = string.Empty;
    }
}
