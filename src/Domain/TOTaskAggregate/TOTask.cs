using Domain.Common.Models;
using Domain.Enums.TOTask;
using Domain.TOTaskAggregate.Events;
using Domain.UserAggregate;

namespace Domain.TOTaskAggregate
{
    public class TOTask : AggregateRoot<TOTaskId>
    {
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public TOTaskStatus Status { get; private set; } = TOTaskStatus.Open;
        public TOTaskPriority Priority { get; private set; } = TOTaskPriority.None;
        public DateTimeOffset? DueDate { get; private set; }
        public virtual TOTaskLabel? Label { get; private set; } = null;
        public virtual User Creator { get; private set; } = default!;
        public virtual User? AssignedTo { get; private set; }
        public virtual List<TOTask> BlockedBy { get; private set; } = [];
        public virtual List<TOTask> Blocks { get; private set; } = [];

        private TOTask(
            TOTaskId taskId,
            string title,
            string description,
            TOTaskStatus status,
            TOTaskLabel? label,
            TOTaskPriority priority,
            DateTimeOffset? dueDate,
            User creator,
            User? assignedTo,
            List<TOTask> blockedBy,
            List<TOTask> blocks
            ) : base(taskId)
        {
            Title = title;
            Description = description;
            Status = status;
            Label = label;
            Priority = priority;
            DueDate = dueDate;
            Creator = creator;
            AssignedTo = assignedTo;
            BlockedBy = blockedBy;
            Blocks = blocks;
        }

        protected TOTask() : base(TOTaskId.CreateUnique()) // Required for EF
        {
        }

        public static TOTask Create(
            string title,
            string description,
            TOTaskStatus status,
            TOTaskLabel? label,
            TOTaskPriority priority,
            DateTimeOffset? dueDate,
            User creator,
            User? assignedTo,
            List<TOTask> blockedBy,
            List<TOTask> blocks)
        {
            TOTask task = new(
                TOTaskId.CreateUnique(),
                title,
                description,
                status,
                label,
                priority,
                dueDate,
                creator,
                assignedTo,
                blockedBy,
                blocks);

            task.AddDomainEvent(new TOTaskCreated(task));

            return task;
        }
    }
}
