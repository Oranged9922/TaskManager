namespace Domain.Common.Models
{
    /// <summary>
    /// Base class for all aggregate roots.
    /// </summary>
    /// <typeparam name="TId"> The type of the id.</typeparam>
    public abstract class AggregateRoot<TId> : Entity<TId>
                           where TId : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
        /// </summary>
        /// <param name="id"></param>
        protected AggregateRoot(TId id)
          : base(id)
        {
        }
    }
}
