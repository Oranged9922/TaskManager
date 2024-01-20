using Domain.TOProjectAggregate.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TOProjectLogic.Events
{
    internal class TOProjectCreatedEventHandler : INotificationHandler<TOProjectCreated>
    {
        public Task Handle(TOProjectCreated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
