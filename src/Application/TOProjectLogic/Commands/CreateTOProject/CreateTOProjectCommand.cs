using ErrorOr;
using MediatR;

namespace Application.TOProjectLogic.Commands.CreateTOProject
{
    public record CreateTOProjectCommand(string Name, string Description) : IRequest<ErrorOr<CreateTOProjectCommandResponse>>;
}
