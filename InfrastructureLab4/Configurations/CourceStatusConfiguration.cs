using DomainLab3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLab4.Configurations
{
    public class CourceStatusConfiguration : IEntityTypeConfiguration<CourceStatus>
    {
        public void Configure(EntityTypeBuilder<CourceStatus> builder)
        {
            builder.ToTable("cource_status");
            builder.HasIndex(cs => cs.EnrollmentId).IsUnique(true);
        }
    }
}
