using Application.TOProjectLogic.Commands.CreateTOProject;
using ErrorOr;
using MediatR;

namespace Application.TOTaskLogic.Commands.CreateTOTask
{
    public record CreateTOTaskCommand(
        string Name,
        string Description,
        string ProjectId) : IRequest<ErrorOr<CreateTOTaskCommandResponse>>;
}
