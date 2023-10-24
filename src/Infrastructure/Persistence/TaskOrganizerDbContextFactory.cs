using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    internal class TaskOrganizerDbContextFactory : IDesignTimeDbContextFactory<TaskOrganizerDbContext>
    {
        // This is used only for migrations
        public TaskOrganizerDbContext CreateDbContext(string[] args)
        {
            return new(null);
        }
    }
}
