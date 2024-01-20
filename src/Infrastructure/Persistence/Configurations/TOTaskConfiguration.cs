using Domain.TOTaskAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Persistence.Configurations
{

    public class TOTaskConfiguration : IEntityTypeConfiguration<TOProject>
    {
        public void Configure(EntityTypeBuilder<TOProject> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(t => t.Description)
                .HasMaxLength(2000);

            builder.Property(t => t.Status)
                .IsRequired();

            builder.Property(t => t.Priority)
                .IsRequired();

            builder.Property(t => t.DueDate);

            builder.HasOne(t => t.Creator)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey("CreatorId")
                .IsRequired();

            builder.HasOne(t => t.AssignedTo)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey("AssignedToId")
                .IsRequired(false);

            builder.HasMany(t => t.BlockedBy)
                .WithMany(t => t.Blocks)
                .UsingEntity<Dictionary<string, object>>(
                    "TaskBlock",
                    b => b.HasOne<TOProject>().WithMany().HasForeignKey("BlockedById"),
                    b => b.HasOne<TOProject>().WithMany().HasForeignKey("BlocksId")
                );
        }
    }

}
