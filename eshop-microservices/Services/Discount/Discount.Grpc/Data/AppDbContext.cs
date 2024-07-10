
using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Discount.Grpc.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Coupon> Coupons { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                  .HasData(
                      new Coupon
                      {
                          Id = 1,
                          ProductName = "IPhone X",
                          Description = "IPhone Description",
                          Amount = 10
                      },
                      new Coupon
                      {
                          Id = 2,
                          ProductName = "Samaung 10",
                          Description = "Samaung 10 Description",
                          Amount = 20
                      }
                  );
        }

    }
}
