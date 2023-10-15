using Domain.UserAggregate;
using ErrorOr;

namespace Application.Common.Interfaces
{
    public interface IUserRepository
    {
        UserId Add(User user);
        void Delete(UserId id);
        void Delete(User user);
        bool Exists(string username);
        bool Exists(UserId id);
        User? Get(UserId id);
        User? GetByEmail(string email);
        User? GetByUsername(string username);
        ErrorOr<User> GetCurrentUser(string jwtToken);
        void Update(User user);
    }
}
