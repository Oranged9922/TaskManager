﻿using Domain.TOTaskAggregate;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance
{
    public class TaskOrganizerDbContext : DbContext
    {
        public string DbPath { get; private set; }
        public string StoragePath { get; private set; }

        public DbSet<User> Users { get; private set; }
        public DbSet<TOTask> Tasks { get; private set; }

        public TaskOrganizerDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            // include name of the project in the path
            // ensure that the path is created
            var _path = Path.Combine(path, "TaskOrganizer");
            Directory.CreateDirectory(_path);

            var __path = Path.Combine(path, "TaskOrganizer", "Storage");
            Directory.CreateDirectory(__path);

            DbPath = Path.Combine(path, "TaskOrganizer", "TaskOrganizer.db");
            StoragePath = Path.Combine(path, "TaskOrganizer", "Storage");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseLazyLoadingProxies();
            options.UseSqlite($"Data Source={DbPath}");
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TOTask>()
                .Property(e => e.Id)
                .HasConversion(
                v => v.Value,
                v => new TOTaskId(v));

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .HasConversion(
                v => v.Value,
                v => new UserId(v));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskOrganizerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
