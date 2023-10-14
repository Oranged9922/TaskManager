using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configurations
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Role)
                .IsRequired();

            builder.HasMany(u => u.AssignedTasks)
                .WithOne(t => t.AssignedTo)
                .HasForeignKey("AssignedToId");

            builder.HasMany(u => u.CreatedTasks)
                .WithOne(t => t.Creator)
                .HasForeignKey("CreatorId");
        }
    }

}
