using Domain.UserAggregate.Events;
using MediatR;

namespace Application.UserLogic.Events
{
    internal class UserCreatedEventHandler : INotificationHandler<UserCreated>
    {
        public Task Handle(UserCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
