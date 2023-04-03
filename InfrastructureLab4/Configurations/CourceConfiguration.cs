using DomainLab3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLab4.Configurations
{
    public class CourceConfiguration : IEntityTypeConfiguration<Cource>
    {
        public void Configure(EntityTypeBuilder<Cource> builder)
        {
            builder.ToTable(nameof(Cource));
            builder.HasKey(rsf => rsf.Id);
            //builder.Property(rsf => rsf.Id).HasColumnName("RecipeId");
            //builder
            //    .HasOne(r => r.Owner)
            //    .WithMany(u => u.Recipes)
            //    .HasForeignKey(r => r.OwnerId);
        }
    }
}
