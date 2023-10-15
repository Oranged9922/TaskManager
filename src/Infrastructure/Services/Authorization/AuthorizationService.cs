using Application.Common.Interfaces;
using Domain.UserAggregate;

namespace Infrastructure.Services.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool Authorize(User user, string policyName)
        {
            if (user is null) return false;
            return user.Role.ToString() == policyName;
        }
    }
}
