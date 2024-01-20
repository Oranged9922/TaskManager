using Domain.Common.Models;

namespace Domain.TOTaskAggregate.Events
{
    public record TOTaskCreated(TOProject TOTask) : IDomainEvent;
}
