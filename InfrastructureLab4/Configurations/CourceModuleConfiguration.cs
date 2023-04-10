using DomainLab3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLab4.Configurations
{
    public class CourceModuleConfiguration : IEntityTypeConfiguration<CourceModule>
    {
        public void Configure(EntityTypeBuilder<CourceModule> builder)
        {
            builder.ToTable("cource_module_status");
            builder.HasKey(cm => cm.ModuleId);
        }
    }
}
