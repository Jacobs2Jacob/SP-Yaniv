using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL.EF
{
    public class DesignTimeStorageDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        internal protected IConfigurationRoot _config;

        public DesignTimeStorageDbContextFactory()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(_config.GetConnectionString("DefaultConnection"))
                .Options;

            return new AppDbContext(options);
        }
    }
}