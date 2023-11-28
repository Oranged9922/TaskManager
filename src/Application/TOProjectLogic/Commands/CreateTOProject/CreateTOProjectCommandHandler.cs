using ErrorOr;
using MediatR;

namespace Application.TOProjectLogic.Commands.CreateTOProject
{
    public class CreateTOProjectCommandHandler : IRequestHandler<CreateTOProjectCommand, ErrorOr<CreateTOProjectCommandResponse>>
    {
        public Task<ErrorOr<CreateTOProjectCommandResponse>> Handle(CreateTOProjectCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
