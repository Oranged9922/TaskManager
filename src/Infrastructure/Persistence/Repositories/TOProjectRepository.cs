using Application.Common.Interfaces;
using Domain.TOProjectAggregate;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal class TOProjectRepository(TaskOrganizerDbContext context) : ITOProjectRepository
    {
        private readonly TaskOrganizerDbContext _context = context;

        public ICollection<TOProject> GetAllUserCreatedProjects(UserId id)
        {
            return [.. _context.Projects.Where(x => x.Creator.Id == id)];
        }

        public TOProject? GetProjectById(TOProjectId id)
        {
            return _context.Projects
                .Include(x => x.Creator)
                .Include(x => x.Cycles)
                .Include(x => x.Labels)
                .Include(x => x.Members)
                // Add more includes here
                .Include(x => x.Tasks)
                .ThenInclude(x => x.Creator)

                .FirstOrDefault(t => t.Id == id);
        }

        public TOProjectId Add(TOProject project)
        {
            _context.Add(project);
            _context.SaveChanges();
            return project.Id;
        }

        public void Update(TOProject project)
        {
            _context.Update(project);
            _context.SaveChanges();
        }

        public void Delete(TOProject project)
        {
            _context.Remove(project);
            _context.SaveChanges();
        }

        public void Delete(TOProjectId id)
        {
            var project = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (project is not null)
            {
                _context.Remove(project);
                _context.SaveChanges();
            }
        }
    }
}
