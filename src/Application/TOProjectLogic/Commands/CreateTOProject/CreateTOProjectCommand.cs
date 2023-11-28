using ErrorOr;
using MediatR;

namespace Application.TOProjectLogic.Commands.CreateTOProject
{
    public record CreateTOProjectCommand() : IRequest<ErrorOr<CreateTOProjectCommandResponse>>;
}
