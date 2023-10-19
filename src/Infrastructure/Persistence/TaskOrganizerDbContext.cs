using Domain.Common.Models;
using Domain.TOTaskAggregate;
using Domain.UserAggregate;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class TaskOrganizerDbContext : DbContext
    {
        public string? DbPath { get; private set; }
        public string? StoragePath { get; private set; }
        private string? _dbName;
        private readonly bool _useInMemory;

        public DbSet<User> Users { get; private set; }
        public DbSet<TOTask> Tasks { get; private set; }

        private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

        public TaskOrganizerDbContext(
            PublishDomainEventsInterceptor publishDomainEventsInterceptor, 
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
            options.AddInterceptors(_publishDomainEventsInterceptor);
            options.UseLazyLoadingProxies();
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

            modelBuilder
                .Ignore<List<IDomainEvent>>()
                .ApplyConfigurationsFromAssembly(typeof(TaskOrganizerDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
