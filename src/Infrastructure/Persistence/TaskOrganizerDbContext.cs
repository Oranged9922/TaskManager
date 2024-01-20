using Domain.Common.Models;
using Domain.TOCycleAggregate;
using Domain.TOProjectAggregate;
using Domain.TOTaskAggregate;
using Domain.UserAggregate;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{
    public class TaskOrganizerDbContext : DbContext
    {
        public string? DbPath { get; private set; }
        public string? StoragePath { get; private set; }
        private string? _dbName;
        private readonly bool _useInMemory;

        public DbSet<User> Users { get; private set; }
        public DbSet<Domain.TOTaskAggregate.TOProject> Tasks { get; private set; }
        public DbSet<TOTaskLabel> Labels { get; private set; }
        public DbSet<TOCycle> Cycles { get; private set; }
        public DbSet<Domain.TOProjectAggregate.TOProject> Projects { get; private set; }

        private readonly PublishDomainEventsInterceptor? _publishDomainEventsInterceptor;

        public TaskOrganizerDbContext(
            PublishDomainEventsInterceptor? publishDomainEventsInterceptor,
            bool useInMemory = false,
            string? dbName = null)
        {
            _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
            _useInMemory = useInMemory;

            if (!_useInMemory)
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);

                var _path = Path.Combine(path, "TaskOrganizer");
                Directory.CreateDirectory(_path);

                var __path = Path.Combine(path, "TaskOrganizer", "Storage");
                Directory.CreateDirectory(__path);

                DbPath = Path.Combine(path, "TaskOrganizer", "TaskOrganizer.db");
                StoragePath = Path.Combine(path, "TaskOrganizer", "Storage");
            }

            _dbName = dbName ?? "DefaultDbName";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (_useInMemory)
            {
                options.UseInMemoryDatabase(_dbName!);
            }
            else
            {
                options.UseSqlite($"Data Source={DbPath}");
            }
            if (_publishDomainEventsInterceptor != null)
                options.AddInterceptors(_publishDomainEventsInterceptor);
            options.UseLazyLoadingProxies();
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Domain.TOTaskAggregate.TOProject>()
                .Property(e => e.Id)
                .HasConversion(
                v => v.Value,
                v => new Domain.TOTaskAggregate.TOProjectId(v));

            modelBuilder.Entity<User>()
                .Property(e => e.Id)
                .HasConversion(
                v => v.Value,
                v => new UserId(v));

            modelBuilder.Entity<TOCycle>()
                .Property(e => e.Id)
                .HasConversion(
                v => v.Value,
                v => new TOCycleId(v));

            modelBuilder.Entity<Domain.TOProjectAggregate.TOProject>()
                .Property(e => e.Id)
                .HasConversion(
                v => v.Value,
                v => new Domain.TOProjectAggregate.TOProjectId(v));

            modelBuilder.Entity<TOTaskLabel>()
                .Property(e => e.Id)
                .HasConversion(
                v => v.Value,
                v => new TOTaskLabelId(v));

            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(TaskOrganizerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
