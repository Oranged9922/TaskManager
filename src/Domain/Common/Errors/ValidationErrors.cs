using ErrorOr;

namespace Domain.Common.Errors
{

    /// <summary>
    /// Class containing all errors that can occur in the validation process.
    /// </summary>
    public static partial class Validation
    {
        /// <summary>
        /// Representing an error when request token is not valid
        /// </summary>
        public static readonly Error InvalidToken = Error.Validation
            (
                code: "701",
                description: "Request does not contain valid token."
            );

        /// <summary>
        /// Representing an error when user has not required permissions
        /// </summary>
        public static readonly Error UserNotAuthorized = Error.Validation
            (
                code: "702",
                description: "User is not authorized."
            );

        /// <summary>
        /// Representing an error when user provided invalid credentials
        /// </summary>
        public static readonly Error InvalidCredentials = Error.Validation
            (
                code: "703",
                description: "Invalid credentials."
            );
    }
}
