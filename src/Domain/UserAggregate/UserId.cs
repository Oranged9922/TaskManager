using Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public sealed class UserId(Guid value) : ValueObject
    {
        /// <summary>
        /// The value of the user id.
        /// </summary>
        public Guid Value { get; private set; } = value;

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        /// <summary>
        /// Creates a unique user id.
        /// </summary>
        /// <returns> The user id.</returns>
        public static UserId CreateUnique()
        {
            return new UserId(Guid.NewGuid());
        }

        /// <summary>
        /// Creates a user id from a guid.
        /// </summary>
        /// <param name="value"> The guid.</param>
        /// <returns> The user id.</returns>
        public static UserId Create(Guid value)
        {
            return new UserId(value);
        }

    }
}
