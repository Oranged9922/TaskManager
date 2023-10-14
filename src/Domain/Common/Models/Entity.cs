namespace Domain.Common.Models
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    /// <typeparam name="TId"> The type of the id.</typeparam>
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
        /// </summary>
        /// <param name="id"> The id.</param>
        protected Entity(TId id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public TId Id { get; private set; }

        /// <summary>
        /// Checks if two entities are equal.
        /// </summary>
        /// <param name="left"> The left entity.</param>
        /// <param name="right"> The right entity.</param>
        /// <returns> True if equal, false otherwise.</returns>
        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Checks if two entities are not equal.
        /// </summary>
        /// <param name="left"> The left entity.</param>
        /// <param name="right"> The right entity.</param>
        /// <returns> True if not equal, false otherwise.</returns>
        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Checks if two entities are equal.
        /// </summary>
        /// <param name="obj"> The object to compare to.</param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            return obj is Entity<TId> entity && Id.Equals(entity.Id);
        }

        /// <summary>
        /// Checks if two entities are equal.
        /// </summary>
        /// <param name="other"> The other entity.</param>
        /// <returns> True if equal, false otherwise.</returns>
        public bool Equals(Entity<TId>? other)
        {
            return Equals((object?)other);
        }

        /// <summary>
        /// Gets the hash code of the entity.
        /// </summary>
        /// <returns> The hash code.</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
