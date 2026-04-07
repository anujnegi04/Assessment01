using PaymentService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace PaymentService.Data
{
    public class PaymentDbContext:DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>()
                .HasIndex(propa => propa.OrderId)
                .IsUnique();
        }
    }
}
