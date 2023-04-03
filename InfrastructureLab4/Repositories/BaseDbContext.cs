using DomainLab3;
using InfrastructureLab4.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLab4.Repositories
{
    public class BaseDbContext: DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> dbContextOptions)
            : base(dbContextOptions)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //base.OnModelCreating(builder);
            //builder.Entity<Cource>().HasIndex(c => c.Id);

            builder.ApplyConfiguration(new CourceConfiguration());

            //builder.ApplyConfiguration(new UserConfiguration());
            //builder.ApplyConfiguration(new RecipeConfiguration());
            //builder.ApplyConfiguration(new TagConfiguration());
            //builder.ApplyConfiguration(new IngridientConfiguration());
            //builder.ApplyConfiguration(new RecipeTagConfiguration());
            //builder.ApplyConfiguration(new RecipePhotoConfiguration());
            //builder.ApplyConfiguration(new LikeConfiguration());
        }
    }
}
