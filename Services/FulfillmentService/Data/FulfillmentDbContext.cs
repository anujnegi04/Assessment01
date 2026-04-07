using FulfillmentService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FulfillmentService.Data
{
    public class FulfillmentDbContext: DbContext
    {     

            public FulfillmentDbContext(DbContextOptions<FulfillmentDbContext> options) : base(options) { 
            
            }

            public DbSet<Fulfillment> Fulfillments => Set<Fulfillment>();

            protected override void OnModelCreating(ModelBuilder modelBuilder) {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Fulfillment>()
                    .HasIndex(f => f.OrderId)
                    .IsUnique();
            }
        }
    }

