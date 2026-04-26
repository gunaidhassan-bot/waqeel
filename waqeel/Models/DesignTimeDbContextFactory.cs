using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace waqeel.Models
{
    // Enables design-time services (dotnet-ef) to create the DbContext
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // Use the same connection string as in OnConfiguring
            optionsBuilder.UseNpgsql("Host=localhost;Database=WaqeelDB;Username=postgres;Password=123");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
