using Domain.Common.Models;

namespace Domain.UserAggregate.Events
{
   public record UserCreated(User User) : IDomainEvent;
}
