using Domain.Common.Models;

namespace Domain.TOTaskAggregate
{
    public class TOProjectId(Guid value) : ValueObject
    {
        /// <summary>
        /// The value of the task id.
        /// </summary>
        public Guid Value { get; private set; } = value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Creates a unique task id.
        /// </summary>
        /// <returns> The task id.</returns>
        public static TOProjectId CreateUnique()
        {
            return new TOProjectId(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a task id from a guid.
        /// </summary>
        /// <param name="value"> The guid.</param>
        /// <returns> The task id.</returns>
        public static TOProjectId Create(Guid value)
        {
            return new TOProjectId(value);
        }
    }
}
