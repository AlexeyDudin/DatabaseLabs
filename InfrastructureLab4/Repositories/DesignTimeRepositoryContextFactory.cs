using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InfrastructureLab4.Repositories
{
    public class DesignTimeRepositoryContextFactory : IDesignTimeDbContextFactory<BaseDbContext>
    {
        public BaseDbContext CreateDbContext(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DbConnection4");
            var optionsBuilder = new DbContextOptionsBuilder<BaseDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BaseDbContext(optionsBuilder.Options);
        }
    }
}
