using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class DbContextFactory
    {  
        internal protected IConfigurationRoot _config;

        public DbContextFactory() 
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public AppDbContext CreateDbContext()
        { 
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(_config.GetConnectionString("DefaultConnection"))
                .Options;

            return new AppDbContext(options);
        } 
    }
}