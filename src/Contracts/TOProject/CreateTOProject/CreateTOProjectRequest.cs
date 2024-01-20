using MediatR;

namespace Contracts.TOProject.CreateTOProject
{
    public record CreateTOProjectRequest(string Name, string Description);
}
