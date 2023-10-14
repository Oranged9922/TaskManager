﻿using Application.Common.Interfaces;
using Domain.TOTaskAggregate;
using Microsoft.EntityFrameworkCore;
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

        public TOTask? GetTaskById(TOTaskId id)
        {
            return _context.Tasks
                .Include(x => x.BlockedBy)
                .Include(x => x.Blocks)
                .Include(x => x.Creator)
                .Include(x => x.AssignedTo)
                .FirstOrDefault(t => t.Id == id);
        }

        public TOTaskId Add(TOTask task)
        {
            _context.Add(task);
            _context.SaveChanges();
            return task.Id;
        }

        public void Update(TOTask task)
        {
            _context.Update(task);
            _context.SaveChanges();
        }

        public void Delete(TOTask task)
        {
            _context.Remove(task);
            _context.SaveChanges();
        }

        public void Delete(TOTaskId id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task is not null)
            {
                _context.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}