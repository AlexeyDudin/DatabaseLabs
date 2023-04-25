using DomainLab3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace InfrastructureLab4.Configurations
{
    public class CourceMatherialConfiguration : IEntityTypeConfiguration<CourceMatherial>
    {
        public void Configure(EntityTypeBuilder<CourceMatherial> builder)
        {
            builder.ToTable("cource_matherial");
            builder.HasKey(cm => cm.ModuleId).HasName("cource_module_id");
            builder.Property(cm => cm.ModuleId).ValueGeneratedNever();
            builder
                .HasOne(cm => cm.Cource)
                .WithMany(c => c.CourceMatherials)
                .HasForeignKey(r => r.CourceId);

        }
    }
}
