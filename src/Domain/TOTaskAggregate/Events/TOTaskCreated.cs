using Domain.Common.Models;

namespace Domain.TOTaskAggregate.Events
{
    public record TOTaskCreated(TOTask TOTask) : IDomainEvent;
}
