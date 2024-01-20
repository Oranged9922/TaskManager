using Application.Common.Interfaces;
using Application.Extensions;
using Domain.TOProjectAggregate;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.TOProjectLogic.Commands.CreateTOProject
{
    public class CreateTOProjectCommandHandler(
        IUserRepository userRepository,
        ITOProjectRepository projectRepository,
        IHttpContextAccessor httpContextAccessor,
        IDateTimeProvider dateTimeProvider) 
        : 
        IRequestHandler<CreateTOProjectCommand, ErrorOr<CreateTOProjectCommandResponse>>
    {
        public async Task<ErrorOr<CreateTOProjectCommandResponse>> Handle(CreateTOProjectCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            var token = httpContextAccessor.GetJwt()!; // Token has been validated by the middleware
            var user = userRepository.GetCurrentUser(token).Value;

            if (projectRepository.GetAllUserCreatedProjects(user.Id).Any(p => p.Name == request.Name))
            {
                return Errors.Repository.ProjectRepository.ProjectAlreadyExists;
            }

            var project = TOProject.Create(
                name: request.Name,
                description: request.Description,
                creator: user,
                members: [],
                startDate: dateTimeProvider.Now,
                endDate: default,
                tasks: [],
                labels: [],
                cycles: []);

            var id = projectRepository.Add(project);

            return new CreateTOProjectCommandResponse(id.Value, project.Name);
        }
    }
}
