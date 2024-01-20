using Domain.Common.Models;
using Domain.Enums.TOCycle;
using Domain.TOCycleAggregate.Events;

namespace Domain.TOCycleAggregate
{
    public class TOCycle : AggregateRoot<TOCycleId>
    {
        public int Number { get; private set; }
        public TOCycleStatus Status { get; private set; }
        public TOCycleType Type { get; private set; }

        public TOCycle(
            TOCycleId id,
            int number,
            TOCycleStatus status,
            TOCycleType type) : base(id)
        {
            Number = number;
            Status = status;
            Type = type;
        }

        public static TOCycle Create(
            int number,
            TOCycleStatus status,
            TOCycleType type)
        {
            var cycle = new TOCycle(TOCycleId.CreateUnique(),
                number, status, type);

            cycle.AddDomainEvent(new TOCycleCreated(cycle));
            return cycle;
        }

        protected TOCycle() : base(TOCycleId.CreateUnique()) // Required for EF
        {
        }
    }
}
