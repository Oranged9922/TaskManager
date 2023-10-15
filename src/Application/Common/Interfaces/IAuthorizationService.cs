using Domain.UserAggregate;

namespace Application.Common.Interfaces
{
    public interface IAuthorizationService
    {
        bool Authorize(User user, string policyName);
    }
}
