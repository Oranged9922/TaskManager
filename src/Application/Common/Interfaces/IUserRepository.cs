using Domain.UserAggregate;
using ErrorOr;

namespace Application.Common.Interfaces
{
    public interface IUserRepository
    {
        ErrorOr<User> GetCurrentUser(string jwtToken);
    }
}
