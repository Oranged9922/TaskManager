using Domain.Common.Models;

namespace Domain.TOTaskAggregate
{
    public class TOTaskLabelId(Guid value) : ValueObject
    {
        /// <summary>
        /// The value of the task label id.
        /// </summary>
        public Guid Value { get; private set; } = value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Creates a task label id from a guid.
        /// </summary>
        /// <returns>The label id</returns>
        public static TOTaskLabelId CreateUnique()
        {
            return new TOTaskLabelId(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a task label id from a guid.
        /// </summary>
        /// <param name="value">The guid.</param>
        /// <returns>The label id.</returns>
        public static TOTaskLabelId Create(Guid value)
        {
            return new TOTaskLabelId(value);
        }
    }
}