using Domain.TOTaskAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class TOTaskLabelConfiguration : IEntityTypeConfiguration<TOTaskLabel>
    {
        public void Configure(EntityTypeBuilder<TOTaskLabel> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Name).IsRequired();

        }
    }
}
