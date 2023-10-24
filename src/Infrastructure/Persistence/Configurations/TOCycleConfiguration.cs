using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.TOCycleAggregate;

namespace Infrastructure.Persistence.Configurations
{
    public class TOCycleConfiguration : IEntityTypeConfiguration<TOCycle>
    {
        public void Configure(EntityTypeBuilder<TOCycle> builder)
        {
            builder.HasKey(t => t.Id);
         
            builder.Property(t => t.Number).IsRequired();
            
            builder.Property(t => t.Status).HasConversion<string>().IsRequired();
            
            builder.Property(t => t.Type).HasConversion<string>().IsRequired();

        }
    }
}
