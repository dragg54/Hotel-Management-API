using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Management_API.Data.DBContexts
{
    public class HotelDBContextFactory : IDesignTimeDbContextFactory<HotelDBContext>
    {
        public HotelDBContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<HotelDBContext>();
            optionsBuilder.UseMySql(
                configuration.GetConnectionString("DbConnection"),
                new MySqlServerVersion(new Version(8, 0, 30)) // Replace with your MySQL version
            );

            return new HotelDBContext(optionsBuilder.Options);
        }
    }
}
