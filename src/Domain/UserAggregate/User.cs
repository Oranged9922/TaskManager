using Domain.Common.Models;
using Domain.Enums.User;
using Domain.TOTaskAggregate;
using Domain.UserAggregate.Events;

namespace Domain.UserAggregate
{
    public class User : AggregateRoot<UserId>
    {
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; private set; } = UserRole.Guest;
        public virtual List<TOTask> AssignedTasks { get; private set; } = [];
        public virtual List<TOTask> CreatedTasks { get; private set; } = [];

        private User(UserId id,
            string username,
            string email,
            string passwordHash,
            UserRole role,
            List<TOTask> assignedTasks,
            List<TOTask> createdTasks) : base(id)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            AssignedTasks = assignedTasks;
            CreatedTasks = createdTasks;
        }

        /// <summary>
        /// Required for EF
        /// </summary>
        protected User() : base(UserId.CreateUnique()) // Required for EF
        {
        }

        public static User Create(
            string username, string email, string passwordHash, UserRole role, List<TOTask> assignedTasks, List<TOTask> createdTasks)
        {
            User user = new(
                UserId.CreateUnique(),
                username, email, passwordHash, role, assignedTasks, createdTasks);

            user.AddDomainEvent(new UserCreated(user));

            return user;
        }
    }
}
