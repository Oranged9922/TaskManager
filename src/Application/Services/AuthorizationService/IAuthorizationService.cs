using Domain.UserAggregate;

namespace Application.Services.AuthorizationService
{
    public interface IAuthorizationService
    {
        bool Authorize(User user, string policyName);
    }
}
