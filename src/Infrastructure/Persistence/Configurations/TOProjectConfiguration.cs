using Domain.TOProjectAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class TOProjectConfiguration : IEntityTypeConfiguration<TOProject>
    {
        public void Configure(EntityTypeBuilder<TOProject> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.EndDate);

            builder.HasOne(p => p.Creator).WithMany().HasForeignKey("CreatorId").IsRequired();

            builder.HasMany(p => p.Members).WithMany().UsingEntity(j =>
            {
                j.ToTable("TOProjectMembers");
            });

            builder.HasMany(p => p.Tasks).WithOne().HasForeignKey("TOProjectId");
            builder.HasMany(p => p.Labels).WithOne().HasForeignKey("TOProjectId");
            builder.HasMany(p => p.Cycles).WithOne().HasForeignKey("TOProjectId");

        }
    }
}
