using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FulfillmentService.Data
{
    public class FulfillmentDbContextFactory: IDesignTimeDbContextFactory<FulfillmentDbContext>
    {
        public FulfillmentDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true)
                   .AddJsonFile("appsettings.{environment}.json", optional: true)
                   .Build();

            var connectionString = configuration.GetConnectionString("FulfillmentDb");
            var optionsBuilder = new DbContextOptionsBuilder<FulfillmentDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new FulfillmentDbContext(optionsBuilder.Options);

        }
    }
}
