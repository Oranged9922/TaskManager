using Domain.Common.Models;

namespace Domain.TOProjectAggregate
{
    public class TOProjectId(Guid value) : ValueObject
    {
        /// <summary>
        /// The value of the project id.
        /// </summary>
        public Guid Value { get; private set; } = value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Creates a unique project id.
        /// </summary>
        /// <returns>The project id.</returns>
        public static TOProjectId CreateUnique()
        {
            return new TOProjectId(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a project id from a guid.
        /// </summary>
        /// <param name="value">The guid.</param>
        /// <returns>The project id.</returns>
        public static TOProjectId Create(Guid value)
        {
            return new TOProjectId(value);
        }

    }
}