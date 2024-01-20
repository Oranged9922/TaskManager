using Domain.Common.Models;
using Domain.TOCycleAggregate;
using Domain.TOProjectAggregate.Events;
using Domain.TOTaskAggregate;
using Domain.UserAggregate;

namespace Domain.TOProjectAggregate
{
    public class TOProject : AggregateRoot<TOProjectId>
    {

        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public DateTimeOffset StartDate { get; private set; }
        public DateTimeOffset? EndDate { get; private set; }
        public virtual User Creator { get; private set; } = null!;
        public virtual List<User> Members { get; private set; } = [];
        public virtual List<TOTaskAggregate.TOProject> Tasks { get; private set; } = [];
        public virtual List<TOTaskLabel> Labels { get; private set; } = [];
        public virtual List<TOCycle> Cycles { get; private set; } = [];

        // required by EF Core
        protected TOProject() : base(TOProjectId.CreateUnique())
        {
        }

        private TOProject(
            TOProjectId projectId,
            string name,
            string description,
            User creator,
            List<User> members,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            List<TOTaskAggregate.TOProject> tasks,
            List<TOTaskLabel> labels,
            List<TOCycle> cycles) : base(projectId)
        {
            Name = name;
            Description = description;
            Creator = creator;
            Members = members;
            StartDate = startDate;
            EndDate = endDate;
            Tasks = tasks;
            Labels = labels;
            Cycles = cycles;
        }

        public static TOProject Create(
            string name,
            string description,
            User creator,
            List<User> members,
            DateTimeOffset startDate,
            DateTimeOffset? endDate,
            List<TOTaskAggregate.TOProject> tasks,
            List<TOTaskLabel> labels,
            List<TOCycle> cycles)
        {
            TOProject project = new(
                TOProjectId.CreateUnique(),
                name,
                description,
                creator,
                members,
                startDate,
                endDate,
                tasks,
                labels,
                cycles);

            project.AddDomainEvent(new TOProjectCreated(project));

            return project;
        }
    }
}
