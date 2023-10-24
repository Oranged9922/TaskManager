using Domain.Common.Models;

namespace Domain.TOCycleAggregate
{
    public class TOCycleId(Guid value) : ValueObject
    {
        /// <summary>
        /// The value of the cycle id.
        /// </summary>
        public Guid Value { get; private set; } = value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Creates a unique cycle id
        /// </summary>
        /// <returns>The cycle id</returns>
        public static TOCycleId CreateUnique()
        {
            return new TOCycleId(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a cycle id from a guid.
        /// </summary>
        /// <param name="value">The guid.</param>
        /// <returns>THe cycle id.</returns>
        public static TOCycleId Create(Guid value)
        {
            return new TOCycleId(value);
        }
    }
}