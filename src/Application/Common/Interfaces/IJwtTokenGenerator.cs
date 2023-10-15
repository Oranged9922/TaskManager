using Domain.UserAggregate;

namespace Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, UserRole role);
    }
}
