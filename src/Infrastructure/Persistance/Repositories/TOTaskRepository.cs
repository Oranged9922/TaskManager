using Application.Common.Interfaces;
using Domain.TOTaskAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Repositories
{
    internal class TOTaskRepository : ITOTaskRepository
    {
        private readonly TaskOrganizerDbContext _context;

        public TOTaskRepository(TaskOrganizerDbContext context)
        {
            _context = context;
        }

        public List<TOTask> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }
    }
}
