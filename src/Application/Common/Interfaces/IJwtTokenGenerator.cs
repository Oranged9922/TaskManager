using Domain.Enums.User;
using Domain.UserAggregate;

namespace Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, UserRole role);
    }
}
