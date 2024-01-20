
using Domain.TOProjectAggregate;
using Domain.UserAggregate;

namespace Application.Common.Interfaces
{
    public interface ITOProjectRepository
    {
        TOProjectId Add(TOProject project);
        ICollection<TOProject> GetAllUserCreatedProjects(UserId id);
        void Delete(TOProject project);
        void Delete(TOProjectId id);
        TOProject? GetProjectById(TOProjectId id);
        void Update(TOProject task);
    }
}
