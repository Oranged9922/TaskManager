using Domain.UserAggregate;

namespace Application.Services.AuthorizationService
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
