using Application.Common.Interfaces;
using Application.Extensions;
using Domain.Enums.TOTask;
using Domain.TOProjectAggregate;
using Domain.TOTaskAggregate;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.TOTaskLogic.Commands.CreateTOTask
{
    public class CreateTOTaskCommandHandler
        (
        IUserRepository userRepository,
        ITOProjectRepository projectRepository,
        IHttpContextAccessor httpContextAccessor)
        : IRequestHandler<CreateTOTaskCommand, ErrorOr<CreateTOTaskCommandResponse>>
    {
        public async Task<ErrorOr<CreateTOTaskCommandResponse>> Handle(CreateTOTaskCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var token = httpContextAccessor.GetJwt()!; // Token has been validated by the middleware
            var user = userRepository.GetCurrentUser(token).Value;

            var project = projectRepository.GetProjectById(new TOProjectId(Guid.Parse(request.ProjectId)));

            if (project is null)
            {
                return Errors.Repository.EntityDoesNotExist;
            }

            if (project.Creator != user && !project.Members.Any(m => m.Id == user.Id))
            {
                return Errors.Repository.ProjectRepository.UserNotMemberOfProject;
            }

            var task = TOTask.Create(
                title: request.Name,
                description: request.Description,
                status: TOTaskStatus.Open,
                label: null,
                priority: TOTaskPriority.None,
                dueDate: null,
                creator: user,
                assignedTo: null,
                blockedBy: [],
                blocks: []);

            project.Tasks.Add(task);

            projectRepository.Update(project);

            return new CreateTOTaskCommandResponse(task.Id.Value.ToString(), task.Title);
        }
    }
}
