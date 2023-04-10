using DomainLab3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLab4.Configurations
{
    public class CourceConfiguration : IEntityTypeConfiguration<Cource>
    {
        public void Configure(EntityTypeBuilder<Cource> builder)
        {
            builder.ToTable("cources");
            builder.Property(r => r.Id).HasColumnName("column_id");
            builder.HasKey(c => c.Id);
        }
    }
}
