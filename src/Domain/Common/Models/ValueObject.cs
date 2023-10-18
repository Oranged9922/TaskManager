namespace Domain.Common.Models
{
    /// <summary>
    /// Base class for value objects.
    /// </summary>
    public abstract class ValueObject
    {
        /// <summary>
        /// Checks if two ValueObjects are equal.
        /// </summary>
        /// <param name="left"> The left ValueObject.</param>
        /// <param name="right"> The right ValueObject.</param>
        /// <returns> Whether the ValueObjects are equal.</returns>
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks if two ValueObjects are not equal.
        /// </summary>
        /// <param name="left"> The left ValueObject.</param>
        /// <param name="right"> The right ValueObject.</param>
        /// <returns> Whether the ValueObjects are not equal.</returns>
        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Returns properties of a ValueObject in defined order.
        /// </summary>
        /// <returns>Properties in a defined order.</returns>
        public abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Checks if two ValueObjects are equal.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject valueObject = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        /// <summary>
        /// Returns the hashcode of a ValueObject.
        /// </summary>
        /// <returns> The hashcode of a ValueObject.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Checks if two ValueObjects are equal.
        /// </summary>
        /// <param name="other"> The other ValueObject.</param>
        /// <returns> Whether the ValueObjects are equal.</returns>
        public bool Equals(ValueObject? other)
        {
            return Equals((object?)other);
        }
    }
}
