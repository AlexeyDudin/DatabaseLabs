using DomainLab3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLab4.Configurations
{
    public class CourceEnrollmentConfiguration : IEntityTypeConfiguration<CourceEnrollment>
    {
        public void Configure(EntityTypeBuilder<CourceEnrollment> builder)
        {
            builder.ToTable("cource_enrollments");
            builder.HasKey(c => c.Id);
            builder
                .HasOne(e => e.Cource)
                .WithMany(c => c.CourceEnrollments)
                .HasForeignKey(r => r.CourceId);
            builder
                .HasOne(e => e.CourceStatus)
                .WithMany(s => s.CourceEnrollments)
                .HasForeignKey(r => r.EnrollmentId);
            builder
                .HasOne(e => e.CourceModule)
                .WithOne(cm => cm.Enrollment);
            //builder
            //    .HasOne(e => e.CourceModule)
            //    .WithMany(cm => cm.Enrollments)
            //    .HasForeignKey(r => r.EnrollmentId);
        }
    }
}
