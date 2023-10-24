using Domain.Common.Models;

namespace Domain.TOTaskAggregate
{
    public class TOTaskLabel : Entity<TOTaskLabelId>
    {
        public string Name { get; private set; }

        // required by EF Core
        protected TOTaskLabel() : base(TOTaskLabelId.CreateUnique())
        {
        }

        private TOTaskLabel(
            TOTaskLabelId labelId,
            string name
            ) : base(labelId)
        {
            Name = name;
        }
    }
}
