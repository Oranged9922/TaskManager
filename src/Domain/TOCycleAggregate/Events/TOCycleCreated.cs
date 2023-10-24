using Domain.Common.Models;

namespace Domain.TOCycleAggregate.Events
{
    public record TOCycleCreated(TOCycle TOCycle) : IDomainEvent;
}