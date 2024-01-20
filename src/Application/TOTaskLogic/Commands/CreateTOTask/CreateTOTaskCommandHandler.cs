using ErrorOr;
using MediatR;

namespace Application.TOTaskLogic.Commands.CreateTOTask
{
    public class CreateTOTaskCommandHandler : IRequestHandler<CreateTOTaskCommand, ErrorOr<CreateTOTaskCommandResponse>>
    {
        public Task<ErrorOr<CreateTOTaskCommandResponse>> Handle(CreateTOTaskCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
