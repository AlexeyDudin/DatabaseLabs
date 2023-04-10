using DomainLab3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLab4.Configurations
{
    public class CourceMatherialConfiguration : IEntityTypeConfiguration<CourceMatherial>
    {
        public void Configure(EntityTypeBuilder<CourceMatherial> builder)
        {
            builder.ToTable("cource_matherial");
            builder.HasKey(cm => cm.ModuleId);
            builder
                .HasOne(cm => cm.CourceModule)
                .WithMany(cm => cm.Matherials)
                .HasForeignKey(r => r.ModuleId);
            builder
                .HasOne(cm => cm.Cource)
                .WithMany(c => c.CourceMatherials)
                .HasForeignKey(r => r.ModuleId);
        }
    }
}
