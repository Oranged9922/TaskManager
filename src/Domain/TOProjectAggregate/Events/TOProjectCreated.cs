using Domain.Common.Models;

namespace Domain.TOProjectAggregate.Events
{
    public record TOProjectCreated(TOProject TOProject) : IDomainEvent;
}
