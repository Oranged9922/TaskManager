﻿using ErrorOr;

namespace Domain.Common.Errors
{
    /// <summary>
    /// Class containing all errors that can occur in the repository.
    /// </summary>
    public static partial class Repository
    {
        /// <summary>
        /// Representing an error when an entity is not present in the database
        /// </summary>
        public static readonly Error EntityDoesNotExist = Error.Failure
            (
                code: "801",
                description: "Entity with given Id is not in the database."
            );

        public static partial class UserRepository
        {
            /// <summary>
            /// Representing an error when a user already exists in the database.
            /// </summary>
            public static readonly Error UsernameAlreadyExists = Error.Failure
            (
                code: "802",
                description: "User with given username already exists in the database."
            );

            /// <summary>
            /// Representing an error when a user a user already exists in the database.
            /// </summary>
            public static readonly Error UserWithEmailAlreadyExists = Error.Failure
            (
                code: "803",
                description: "User with given email already exists in the database."
            );
        }

    }
}
