using Domain.Common.Models;

namespace Domain.UserAggregate.Events
{
    public record UserLoggedIn(User User) : IDomainEvent;
}
