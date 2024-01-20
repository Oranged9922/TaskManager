using Domain.Common.Models;
using Domain.UserAggregate;

namespace Domain.TOProjectAggregate.Events
{
    public record TOProjectCreated(TOProject TOProject, User Creator) : IDomainEvent;
}
